// Author: Nikola Machálková

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JustNote.App.Views
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(3000);
            MainWindow mW = new MainWindow();
            mW.Show();
            this.Close();
        }

    }
}
