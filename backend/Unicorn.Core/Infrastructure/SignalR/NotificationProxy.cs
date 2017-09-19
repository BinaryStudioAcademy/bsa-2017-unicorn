using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Infrastructure.SignalR
{
    public class NotificationProxy : INotificationProxy
    {
        public NotificationProxy(IHubContext context)
        {
            _context = context;
        }

        public async Task SendNotification(long accountId, NotificationDTO notification)
        {
            await _context.Clients.Group($"accountId={accountId}").OnNotificationRecieved(notification);
        }

        public async Task RefreshOrdersForAccount(long accountId)
        {
            await _context.Clients.Group($"accountId={accountId}").RefreshOrders();
        }

        public async Task KickAccount(long accountId)
        {
            await _context.Clients.Group($"accountId={accountId}").SignOut();
        }

        public async Task RefreshMessagesForAccount<T>(long accountId, T payload)
        {
            await _context.Clients.Group($"accountId={accountId}").RefreshMessages(payload);
        }

        public async Task RefreshCalendarsEvents<T>(long accountId, T payload)
        {
            await _context.Clients.Group($"accountId={accountId}").RefreshCalendarsEvents(payload);
        }

        public async Task RefreshAdminFeedbacks<T>(long accountId, T payload)
        {
            await _context.Clients.Group($"accountId={accountId}").RefreshAdminFeedbacks(payload);
        }

        public async Task ReadNotReadedMessages(long accountId, long dialogId)
        {
            await _context.Clients.Group($"accountId={accountId}").ReadNotReadedMessages(dialogId);
        }
        public async Task DeleteMessage(long accountId, long dialogId)
        {
            await _context.Clients.Group($"accountId={accountId}").DeleteMessage(dialogId);
        }


        private IHubContext _context;
    }
}
