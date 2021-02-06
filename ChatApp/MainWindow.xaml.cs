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
using ChatApp.Services;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var password = Password.Password;

            var loginResult = await ServerService.GetUser(login, password);
            if (loginResult.Success)
            {
                SwitchToChat(login);
            }
            else
            {
                var text = (string)loginResult.Value;
                var caption = "Login fault";
                var button = MessageBoxButton.OK;
                MessageBox.Show(text, caption, button);
            }
        }

        private void SwitchToRegister(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Show();
            this.Close();
        }

        private void SwitchToChat(string login)
        {
            UserService.SetLogin(login);
            var chat = new Chat();
            chat.Show();
            this.Close();
        }
    }
}
