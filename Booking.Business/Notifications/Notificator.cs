using Booking.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Notifications
{
    public class Notificator : INotificator
    {
        public List<Notification> _notifications { get; set; }

        public Notificator()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool IsThereNotification()
        {
            return _notifications.Any();
        }
    }
}
