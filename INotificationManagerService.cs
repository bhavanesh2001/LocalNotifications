using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications
{
   public interface INotificationManagerService
{
    event EventHandler<NotificationRecievedEventArgs> NotificationReceived;
    void SendNotification(string title, string message, DateTime? notifyTime = null);

}
}
