
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
    public class SettingViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private bool _settingViewVisible = false;
        private TextSettingsViewModel _textSettingsViewModel;
        private ExportViewModel _exportViewModel;

        public SettingViewModel(IDataService dataService)
        {
            _dataService = dataService;
            HideSettingCommand = new RelayCommand(HideSetting);
            Mediator.Register("SettingVisible", SettingVisible);
        }

        public TextSettingsViewModel TextSettingsViewModel
        {
            get { return _textSettingsViewModel; }
            set
            {
                _textSettingsViewModel = value;
                OnPropertyChanged();
            }
        }

        public ExportViewModel ExportViewModel
        {
            get { return _exportViewModel; }
            set
            {
                _exportViewModel = value;
                OnPropertyChanged();
            }
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
    }
}
