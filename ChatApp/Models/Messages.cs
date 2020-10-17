﻿using System;
using System.Collections.Generic;

namespace ChatApp.Models
{
    public partial class Messages
    {
        public Messages(string content, string login)
        {
            Content = content;
            Login = login;
            TimeSend = DateTime.Now;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Content { get; set; }
        public DateTime TimeSend { get; set; }
    }
}