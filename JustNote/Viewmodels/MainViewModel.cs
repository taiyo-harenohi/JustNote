
// Author: Lukáš Leták
// Date: 9/12/2022

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
        private Data _data;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            FetchDateData = new RelayCommand<DateTime>( date => UpdateDateData(date, null));
            CanvasLClick = new RelayCommand<System.Windows.IInputElement>( Canvas => CreateTextbox(Canvas));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


        public ICommand FetchDateData { get; }
        
        public ICommand CanvasLClick { get; }

        private void CreateTextbox(System.Windows.IInputElement DateCanvas)
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



        private void UpdateDateData(DateTime date, string title)
        {
            DateData = _dataService.GetDateData(date,title);
        }

    }
}
