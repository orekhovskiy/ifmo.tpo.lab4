using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace ChatApp
{
    public partial class Chat : Window
    {
        private readonly ObservableCollection<ViewMessage> _messages;
        private HubConnection _connection;

        public Chat()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CurrentLogin.Text = UserService.GetLogin();
            ScrollViewer.ScrollToEnd();

            _messages = new ObservableCollection<ViewMessage>();
            Messages.ItemsSource = _messages;

            StartConnection();
        }

        private async void StartConnection()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(UrlService.GetHubUrl())
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            _connection.On<List<object>>("UpdateMessages", (messages) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    _messages.Clear();
                    foreach (var message in messages)
                    {
                        var deserializedMessage = JsonConvert.DeserializeObject<Messages>(message.ToString());
                        var daySent = deserializedMessage.TimeSend.Date.Equals(DateTime.Today.Date) 
                            ? "today" 
                            : deserializedMessage.TimeSend.ToShortDateString();
                        var viewMessage = new ViewMessage(
                            deserializedMessage.Content,
                            deserializedMessage.Login,
                            daySent);
                        _messages.Add(viewMessage);
                    }
                });
            });

            _connection.On<string>("ReceiveError", (error) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var text = error;
                    var caption = "Send message fault";
                    var button = MessageBoxButton.OK;
                    MessageBox.Show(text, caption, button);
                });
            });

            try
            {
                await _connection.StartAsync();
                await _connection.InvokeAsync("GetAllMessages");
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                var caption = "Connection fault";
                var button = MessageBoxButton.OK;
                MessageBox.Show(text, caption, button);
            }
        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {
            var content = Message.Text;
            var login = UserService.GetLogin();

            await _connection.InvokeAsync("PostMessage", content, login);

            Message.Text = "";
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            UserService.SetLogin("");
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
