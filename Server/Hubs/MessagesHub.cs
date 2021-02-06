using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Services;

namespace Server.Hubs
{
    public class MessagesHub : Hub
    {
        private readonly IDatabaseService _databaseService;

        public MessagesHub(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task PostMessage(string content, string login)
        {
            var result = _databaseService.AddMessage(content, login);
            if (result.Success)
            {
                var allMessages = _databaseService.GetAllMessages();
                await Clients.All.SendAsync("UpdateMessages", allMessages);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveError", (string) result.Value);
            }
        }

        public async Task GetAllMessages()
        {
            var allMessages = _databaseService.GetAllMessages();
            await Clients.Caller.SendAsync("UpdateMessages", (List<Messages>)allMessages);
        }
    }
}
