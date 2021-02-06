using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class ViewMessage
    {
        public ViewMessage(string content, string login, string daySent)
        {
            Login = login;
            Content = content;
            DaySent = daySent;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Content { get; set; }
        public string DaySent { get; set; }
    }
}
