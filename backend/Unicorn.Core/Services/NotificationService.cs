using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;

using Unicorn.Core.Infrastructure.SignalR;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService(IHubContext context)
        {
            _context = context;
        }

        public async Task SendNotification(long accountId, NotificationDTO notification)
        {
            await _context.Clients.Group($"accountId={accountId}").OnNotificationRecieved(notification);
        }

        private IHubContext _context;
        private static List<ConnectedAccount> ConnectedAccounts = new List<ConnectedAccount>();
    }
}
