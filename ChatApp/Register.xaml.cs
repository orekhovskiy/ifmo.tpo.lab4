using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatApp
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void SwitchToLogin(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Signup(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var password = Password.Password;
            var firstname = Firstname.Text;
            var lastname = Lastname.Text;

            var signupResult = DataBaseService.AddUser(login, password, firstname, lastname);
            if (signupResult.Success)
            {
                SwitchToChat(login);
            }
            else
            {
                var text = (string)signupResult.Value;
                var caption = "Registration fault";
                var button = MessageBoxButton.OK;
                MessageBox.Show(text, caption, button);
            }
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
