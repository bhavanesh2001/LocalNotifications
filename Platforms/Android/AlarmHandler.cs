using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class AlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(NotificationManagerService.MessageKey);
                int type = intent.GetIntExtra(NotificationManagerService.NotificationTypeKey,0);
                NotificationManagerService manager = NotificationManagerService.Instance ?? new NotificationManagerService();
                manager.Show(title, message,(NotificationType)type);
            }
        }
    }
}
