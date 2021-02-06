using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Commons;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public UserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [ActionName("GetUser")]
        public Result GetUser([FromQuery] string login, [FromQuery] string password)
        => _databaseService.GetUser(login, password);

        [HttpGet]
        [ActionName("AddUser")]
        public Result AddUser([FromQuery] string login, [FromQuery] string password, [FromQuery] string firstname, [FromQuery] string lastname)
            => _databaseService.AddUser(login, password, firstname, lastname);

        [HttpGet]
        [ActionName("DeleteUserById")]
        public Result DeleteUserById([FromQuery] int id)
            => _databaseService.DeleteUserById(id);

        [HttpGet]
        [ActionName("UserExists")]
        public Result UserExists([FromQuery] string login)
            => _databaseService.UserExists(login);

        [HttpGet]
        [ActionName("AddMessage")]
        public Result AddMessage([FromQuery] string content, [FromQuery] string login)
            => _databaseService.AddMessage(content, login);

        [HttpGet]
        [ActionName("DeleteMessage")]
        public Result DeleteMessage(int id)
            => _databaseService.DeleteMessage(id);

        [HttpGet]
        [ActionName("GetAllMessages")]
        public List<Messages> GetAllMessages()
            => _databaseService.GetAllMessages();
    }
}
