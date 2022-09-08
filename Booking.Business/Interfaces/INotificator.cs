using Booking.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Interfaces
{
    public interface INotificator
    {
        bool IsThereNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
