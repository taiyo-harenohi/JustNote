// Authors:
//  Nikola Machálková – message boxes
//        Lukáš Leták – the rest

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JustNote.App.Services;
using JustNote.Backend.Data;
using Microsoft.Toolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;

namespace JustNote.App.Viewmodels
{
    public class CalendarViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private bool _calendarViewVisible = false;
        private DateTime _selectedDate;
        private string _keyword;

        public CalendarViewModel(IDataService dataService,DateTime NewDate)
        {
            SelectedDate = NewDate;
            _dataService = dataService;
            HideCalendarCommand = new RelayCommand(HideCalendar);
            LoadFilenamesCommand = new RelayCommand(LoadFilenamesInDate);
            OpenNoteCommand = new RelayCommand<string>(_string => OpenNote(_string));
            OpenFoundNoteCommand = new RelayCommand<string[]>(_string => OpenFoundNote(_string));
            FindKeywordCommand = new RelayCommand(FindKeyword);
            NewNoteCommand = new RelayCommand(NewNote);
            Mediator.Register("CalendarVisible", CalendarVisible);
        }

        //Date that is selected in calendar
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SendDateToMain(_selectedDate);
                LoadFilenamesInDate();
                OnPropertyChanged();
            }
        }

        //Keyword to search for
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                OnPropertyChanged();
            }
        }

        //If the calendar menu is shown/hidden
        public bool CalendarViewVisible
        {
            get { return _calendarViewVisible; }
            set
            {
                _calendarViewVisible = value;
                LoadFilenamesInDate();
                OnPropertyChanged();
            }
        }

        //Colection of filenems that are on selected date
        public ObservableCollection<string> Files { get; set; } = new();
        //Colection of found files usin keyword in selected date
        public ObservableCollection<string[]> KWFiles { get; set; } = new();

        public ICommand HideCalendarCommand { get; }

        public ICommand LoadFilenamesCommand { get; }

        public ICommand NewNoteCommand { get; }

        public ICommand OpenNoteCommand { get; }

        public ICommand OpenFoundNoteCommand { get; }

        public ICommand FindKeywordCommand { get; }

        private void HideCalendar()
        {
            CalendarViewVisible = false;
        }

        //change visibility from MainViewModel using mediator
        private void CalendarVisible(object isVisible)
        {
            bool _is = (bool)isVisible;
            if (_is)
            {
                CalendarViewVisible = true;
            }
            else
            {
                CalendarViewVisible = false;
            }
        }

        //Crates a new note whenewer selected date changes
        private static void SendDateToMain(DateTime date)
        {
            Mediator.Send("SetDate", date);
        }
        //Crates new note in main when you click on the plus button next to note (used when you saved a note and want to create new with the same date)
        private void NewNote()
        {
            var result = MessageBox.Show("Do you wish to make a new page?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Mediator.Send("SetDate", SelectedDate);
            }
        }
        
        //Loads all filenemes in selected date
        private void LoadFilenamesInDate()
        {
            if(_dataService != null)
            {
                var filenames = _dataService.GetFilenamesInDate(SelectedDate);
                Files.Clear();
                if (filenames != null)
                {
                    foreach (var name in filenames)
                    {
                        Files.Add(name);
                    }
                }
            }
        }

        //Opens selected date in main
        private void OpenNote(string? title)
        {
            if(title != null)
            {
                Data date_to_open = new();
                date_to_open.Date = SelectedDate;
                date_to_open.Title = title;
                HideCalendar();
                Debug.WriteLine(title);
                Mediator.Send("OpenDate", date_to_open);
            }
        }

        //Opens selected date in main
        private void OpenFoundNote(string[]? title)
        {
            if (title != null)
            {
                Data date_to_open = new();
                date_to_open.Date = DateTime.Parse(title[1]);
                date_to_open.Title = title[0];
                HideCalendar();
                Debug.WriteLine(title);
                Mediator.Send("OpenDate", date_to_open);
            }
        }

        //Finds keyword in selected date and lists all files matchig it
        private void FindKeyword()
        {
            if (_dataService != null)
            {
                var filenames = _dataService.GetFilenamesKeyword(Keyword,SelectedDate);
                KWFiles.Clear();
                if (filenames != null)
                {
                    foreach (var name in filenames)
                    {
                        KWFiles.Add(name);
                    }
                }
                MessageBox.Show($"All files with keyword {Keyword} were found.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
