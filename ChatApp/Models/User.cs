using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class User
    {
        public User(string login, string password, string firstname, string lastname)
        {
            Login = login;
            Password = Commons.Hasher.GetHash(password);
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
