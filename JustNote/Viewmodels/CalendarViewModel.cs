
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
        }

        public ICommand HideCalendarCommand { get; }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SendDateToMain(_selectedDate);
                OnPropertyChanged();
            }
        }

        private void SendDateToMain(DateTime date)
        {
            Mediator.Send("SetDate", date);

        }
        private void HideCalendar()
        {
            CalendarViewVisible = false;
        }

        public bool CalendarViewVisible
        {
            get { return _calendarViewVisible; }
            set
            {
                _calendarViewVisible = value;
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
