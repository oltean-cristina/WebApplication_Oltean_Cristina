using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication_Oltean_Cristina.Hubs
{
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task SendMessage(string user, string message)
            {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
    }
}
