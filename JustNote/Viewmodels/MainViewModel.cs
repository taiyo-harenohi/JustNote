
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
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDataService _dataService;
        private CalendarViewModel _calendarViewModel;
        private SettingViewModel _settingViewModel;
        private Data _data;
        private DateTime _Date;
        private string _Title;
      

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            FetchDateData = new RelayCommand<DateTime>( date => LoadDateData(date, null));
            //CanvasLClick = new RelayCommand<System.Windows.IInputElement>( Canvas => CreateTextbox(Canvas));
            //TODO / null is not correct
            CanvasLDoubleClick = new RelayCommand<System.Windows.IInputElement>(Canvas => CreateTextbox(Canvas, null));
            ShowCalendarCommand = new RelayCommand(ShowCalendar);
            ShowSettingCommand = new RelayCommand(ShowSetting);
            SaveDateDataCommand = new RelayCommand(SaveDateData);
            CalendarViewModel = new CalendarViewModel(dataService, DateTime.Now);
            SettingViewModel = new SettingViewModel(dataService);
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
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
        
        public ICommand SaveDateDataCommand { get; }
        //public ICommand CanvasLClick { get;}
        public ICommand CanvasLDoubleClick { get;}


        private void CreateTextbox(System.Windows.IInputElement DateCanvas, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Mouse.DirectlyOver != DateCanvas)
                    return;
                if (DateCanvas == null)
                    return;
                var mouseX = Mouse.GetPosition(DateCanvas).X;
                var mouseY = Mouse.GetPosition(DateCanvas).Y;
                int[] mouseCoord = { (int)mouseX, (int)mouseY };
                var note = new Note(1, "TEST TEXT", mouseCoord);
                Notes.Add(note);
            }
        }

        public ObservableCollection<Note> Notes { get; set; } = new();


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

        private void ShowCalendar()
        {
            CalendarViewModel.CalendarViewVisible = true;
        }

        private void ShowSetting()
        {
            SettingViewModel.SettingViewVisible = true;
        }

        private void LoadDateData(DateTime date, string title)
        {
            DateData = _dataService.GetDateData(date,title);
            Date = DateData.Date;
            Title = DateData.Title;
            Notes.Clear();
            if(DateData.Notes != null)
            {
                foreach (var note in DateData.Notes)
                {
                    Notes.Add(note);
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
                DateData.Notes.Add(note);
            }
            _dataService.SaveDateData(DateData);
            
        }

    }
}
