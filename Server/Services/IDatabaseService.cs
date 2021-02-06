using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Commons;
using Server.Models;

namespace Server.Services
{
    public interface IDatabaseService
    {
        public Result AddUser(string login, string password, string firstname, string lastname);
        public Result DeleteUserById(int id);
        public Result GetUser(string login, string password);
        public Result UserExists(string login);
        public Result AddMessage(string content, string login);
        public Result DeleteMessage(int id);
        public List<Messages> GetAllMessages();

    }
}
