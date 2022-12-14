// Author: Lukáš Leták

using System;
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
    /// Interaction logic for ViewCalendar.xaml
    /// </summary>
    public partial class ViewCalendar : UserControl
    {
        public ViewCalendar()
        {
            InitializeComponent();

            CalendarViewModel viewModel = new CalendarViewModel(new DataDiskService(),DateTime.Now);

            DataContext = viewModel;
        }
    }
}
