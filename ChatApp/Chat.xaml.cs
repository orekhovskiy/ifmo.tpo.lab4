using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatApp.Models;
using ChatApp.Services;

namespace ChatApp
{
    /// <summary>
    /// Логика взаимодействия для Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        private ObservableCollection<Messages> _messages;

        public Chat()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CurrentLogin.Text = UserService.GetLogin();
            ScrollViewer.ScrollToEnd();

            _messages = new ObservableCollection<Messages>();
            Messages.ItemsSource = _messages;

            UpdateMessages();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            var content = Message.Text;
            var login = UserService.GetLogin();

            var sendMessageResult = DataBaseService.AddMessage(content, login);
            if (sendMessageResult.Success)
            {
                Message.Text = "";
                UpdateMessages();
            }
            else
            {
                var text = (string) sendMessageResult.Value;
                var caption = "Send message fault";
                var button = MessageBoxButton.OK;
                MessageBox.Show(text, caption, button);
            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateMessages();
        }

        private void UpdateMessages()
        {
            _messages.Clear();
            var messagesList = (List<Messages>)DataBaseService.GetAllMessages().Value;
            foreach (var message in messagesList)
            {
                _messages.Add(message);
            }
        }
    }
}
