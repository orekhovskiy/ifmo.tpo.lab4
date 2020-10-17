using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Commons;

namespace ChatApp.Services
{
    public static class DataBaseService
    {
        public static Result AddUser(string login, string password, string firstname, string lastname)
        {
            throw new NotImplementedException();
        }

        public static Result DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public static Result GetUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        public static Result UserExists(string login)
        {
            throw new NotImplementedException();
        }

        public static Result AddMessage(string content, string login)
        {
            throw new NotImplementedException();
        }

        public static Result DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        public static Result GetAllMessages()
        {
            throw new NotImplementedException();
        }
    }
}
