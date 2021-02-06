using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class Messages
    {
        public Messages(string content, string login)
        {
            Login = login;
            Content = content;
            TimeSend = DateTime.Now;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Content { get; set; }
        public DateTime TimeSend { get; set; }
    }
}
