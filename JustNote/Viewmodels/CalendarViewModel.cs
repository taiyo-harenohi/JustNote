
// Author: Lukáš Leták

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
namespace JustNote.App.Viewmodels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private IDataService _dataService;
        private bool _calendarViewVisible = false;
        private DateTime _selectedDate;

        public CalendarViewModel(IDataService dataService,DateTime NewDate)
        {
            SelectedDate = NewDate;
            _dataService = dataService;
            HideCalendarCommand = new RelayCommand(HideCalendar);
            LoadFilenamesCommand = new RelayCommand(LoadFilenamesInDate);
            OpenNoteCommand = new RelayCommand<string>(_string => OpenNote(_string));
        }

        public ICommand HideCalendarCommand { get; }

        public ICommand LoadFilenamesCommand { get; }

        public ICommand OpenNoteCommand { get; }

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

        public ObservableCollection<string> Files { get; set; } = new();

        private void SendDateToMain(DateTime date)
        {
            Mediator.Send("SetDate", date);
        }
        private void HideCalendar()
        {
            CalendarViewVisible = false;
        }

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
                        string[] tmp = {name,"0" };
                        Files.Add(name);
                    }
                }
            }
        }

        private void OpenNote(string title)
        {
            if(title != null)
            {
                Data date_to_open = new();
                date_to_open.Date = SelectedDate;
                date_to_open.Title = title;
                HideCalendar();
                Mediator.Send("OpenDate", date_to_open);
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
