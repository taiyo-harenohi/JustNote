
// Authors: Nikola Machálková (basic layout and commands) Lukáš Leták (mediator)

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
    public class SettingViewModel : INotifyPropertyChanged
    {
        private IDataService _dataService;
        private bool _settingViewVisible = false;

        public SettingViewModel(IDataService dataService)
        {
            _dataService = dataService;
            HideSettingCommand = new RelayCommand(HideSetting);
            Mediator.Register("SettingVisible", SettingVisible);
        }

        public ICommand HideSettingCommand { get; }


        private void HideSetting()
        {
            SettingViewVisible = false;
        }

        private void SettingVisible(object isVisible)
        {
            bool _is = (bool)isVisible;
            if (_is)
            {
                SettingViewVisible = true;
            }
            else
            {
                SettingViewVisible = false;
            }
        }

        public bool SettingViewVisible
        {
            get { return _settingViewVisible; }
            set
            {
                _settingViewVisible = value;
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
