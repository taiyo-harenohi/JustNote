﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JustNote.App.Viewmodels;
using JustNote.App.Services;

namespace JustNote.App.Views
{
    /// <summary>
    /// Interaction logic for ViewSetting.xaml
    /// </summary>
    public partial class ViewSetting : UserControl
    {
        public ViewSetting()
        {
            InitializeComponent();

             SettingViewModel viewModel = new SettingViewModel(new DataDiskService());

            DataContext = viewModel;
        }
    }
}
