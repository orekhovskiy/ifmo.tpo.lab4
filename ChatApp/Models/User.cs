using System;
using System.Collections.Generic;
using ChatApp.Commons;

namespace ChatApp.Models
{
    public partial class User
    {
        public User()
        {

        }

        public User(string login, string password, string firstname, string lastname)
        {
            Login = login;
            Password = Hasher.GetHash(password);
            Firstname = firstname;
            Lastname = lastname;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
