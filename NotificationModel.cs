#if ANDROID
using LocalNotifications.Platforms.Android;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications
{
    public class NotificationModel
    {
       
#if ANDROID
       public required string Title { get; set; }
       public required string Description { get; set; }
       public required NotificationType DisplayType { get; set; }
#endif
    }
}
