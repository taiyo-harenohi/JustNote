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
    public class MainViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private CalendarViewModel _calendarViewModel;
        private SettingViewModel _settingViewModel;
        private Data _data;
        private DateTime _Date;
        private string _Title;
        private int _noteID;


        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _noteID = 0;
            FetchDateData = new RelayCommand<DateTime>( date => LoadDateData(date, null));
            CanvasLDClick = new RelayCommand<System.Windows.IInputElement>( Canvas => CreateTextbox(Canvas));
            CanvasLClick = new RelayCommand<System.Windows.IInputElement>(Canvas => HideMenus(Canvas));
            NoteRemoveCommand = new RelayCommand<int>(key => RemoveTextbox(key));

            ShowCalendarCommand = new RelayCommand(ShowCalendar);
            ShowSettingCommand = new RelayCommand(ShowSetting);
            SaveDateDataCommand = new RelayCommand(SaveDateData);
            DeleteWholeNoteCommand = new RelayCommand(DeleteWholeNote);
            
            Mediator.Register("SetDate", SetDate);
            Mediator.Register("OpenDate", OpenDate);
        }
       

        public CalendarViewModel CalendarViewModel
        {
            get { return _calendarViewModel; }
            set
            {
                _calendarViewModel = value;
                OnPropertyChanged();
            }
        }

        public SettingViewModel SettingViewModel
        {
            get { return _settingViewModel; }
            set
            {
                _settingViewModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NoteViewModel> Notes { get; set; } = new();

        public Data DateData
        {
            get => _data;
            private set
            {
                _data = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _Date;
            set
            {
                _Date = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        private void SetDate(object date)
        {
            if (date == null)
                return;
            DateTime _date = (DateTime)date;
            Date = _date;
            LoadDateData(Date, null);
        }

        private void OpenDate(object note)
        {
            if (note == null)
                return;
            Data _note = (Data)note;
            Date = _note.Date;
            Title = _note.Title;
            LoadDateData(Date, Title);
        }

        public ICommand ShowCalendarCommand { get; }
        public ICommand ShowSettingCommand { get; }

        public ICommand FetchDateData { get; }
        public ICommand DeleteWholeNoteCommand { get; }

        public ICommand NoteRemoveCommand { get; }
        public ICommand SaveDateDataCommand { get; }

        public ICommand CanvasLDClick { get; private set; }
        public ICommand CanvasLClick { get; private set; }

        private static void HideMenus(System.Windows.IInputElement? DateCanvas)
        {
            if (Mouse.DirectlyOver != DateCanvas)
                return;
            if (DateCanvas == null)
                return;
            Mediator.Send("CalendarVisible", false);
            Mediator.Send("SettingVisible", false);
        }

        private void ShowCalendar()
        {
            Mediator.Send("CalendarVisible", true);
        }

        private void ShowSetting()
        {
            Mediator.Send("SettingVisible", true);
        }

        private void CreateTextbox(System.Windows.IInputElement? DateCanvas)
        {
            if (Mouse.DirectlyOver != DateCanvas)
                return;
            if (DateCanvas == null)
                return;
            var mouseX = Mouse.GetPosition(DateCanvas).X;
            var mouseY = Mouse.GetPosition(DateCanvas).Y;
            int[] mouseCoord = { (int)mouseX, (int)mouseY };
            string input = "";
            var note = new Note(_noteID, input, mouseCoord);
            var NoteVM = new NoteViewModel(note);
            Notes.Add(NoteVM);
            _noteID++;
        }

        private void RemoveTextbox(int key)
        {
            var result = MessageBox.Show("Are you sure you want to delete note? This can't be undone.", "Warning", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var note in Notes)
                {
                    if (note.Key == key)
                    {
                        Notes.Remove(note);
                        return;
                    }
                }
                MessageBox.Show("Action was done successfully.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteWholeNote()
        {
            var result = MessageBox.Show("Are you sure you want to delete page? This can't be undone.", "Warning", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _dataService.DeleteDateData(DateData);
                LoadDateData(DateData.Date, null);
                MessageBox.Show("Action was done successfully.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadDateData(DateTime date, string? title)
        {
            DateData = _dataService.GetDateData(date,title);
            Date = DateData.Date;
            Title = DateData.Title;
            Notes.Clear();
            _noteID = 0;
            if(DateData.Notes != null)
            {
                foreach (var note in DateData.Notes)
                {
                    var NoteVM = new NoteViewModel(note);
                    Notes.Add(NoteVM);
                    _noteID++;
                }
            }
        }

        private void SaveDateData()
        {
            DateData.Date = Date;
            DateData.Title = Title;
            if (DateData.Notes == null)
                DateData.Notes = new();
            DateData.Notes.Clear();
            foreach (var note in Notes)
            {
                var normalNote = new Note(note.Key, note.Text, note.Position);
                DateData.Notes.Add(normalNote);
            }
            _dataService.SaveDateData(DateData);

            MessageBox.Show("Note was saved succesfully.", "Notice", System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
