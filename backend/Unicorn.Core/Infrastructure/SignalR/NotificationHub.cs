using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Infrastructure.SignalR
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnected()
        {
            await Groups.Add(Context.ConnectionId, $"accountId={Context.QueryString["accountId"]}");
            await base.OnConnected();
        }

        public override async Task OnReconnected()
        {
            await Groups.Add(Context.ConnectionId, $"accountId={Context.QueryString["accountId"]}");
            await base.OnConnected();
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}
