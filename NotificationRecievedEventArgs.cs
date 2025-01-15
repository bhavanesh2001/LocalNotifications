using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications
{
    public class NotificationRecievedEventArgs : EventArgs
    {
        public required string Title { get; set; }
        public required string Message { get; set; }
    }
}
