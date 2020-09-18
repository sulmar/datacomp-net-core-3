using DataComp.Training.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.SignalRApi.Hubs
{
    public class UsersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            this.Groups.AddToGroupAsync(Context.ConnectionId, "GrupaA");

            return base.OnConnectedAsync();
        }

        public async Task SendAddedUser(User user)
        {
            // await Clients.All.SendAsync("AddedUser", user);

            await Clients.Others.SendAsync("AddedUser", user);

            // await Clients.Groups("GrupaA").SendAsync("AddedUser", user);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("Pong", message);
        }
    }
}
