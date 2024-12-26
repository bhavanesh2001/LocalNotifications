using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications.Platforms.Android
{
    public class AndroidConstants
    {
        public const string SIMPLE_NOTIFICATION_CHANNEL_ID = "1";
        public const string SIMPLE_NOTIFICATION_CHANNEL_NAME = "Simple Notifications";
        public const string SIMPLE_NOTIFICATION_CHANNEL_DESCRIPTION = "This Notification first appears as an icon in the status bar.User can open notification drawer, where they can view more details of notification";
    }

    public enum NotificationType
    {
        SimpleNotification,
        ActionButtonNotification,
        DirectReplyNotification,
        HeadsUpNotification,
        ProgressBarNotification,
        UrgetnNotificaton,
    }
}
