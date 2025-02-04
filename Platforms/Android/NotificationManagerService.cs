using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;

namespace LocalNotifications.Platforms.Android
{
    public class NotificationManagerService : INotificationManagerService
    {
        #region Properties
        public event EventHandler<NotificationRecievedEventArgs>? NotificationReceived;

        const string ChannelId = "default";
        const string ChannelName = "Default";
        const string ChannelDescription = "The default channel for notifications.";

        bool channelInitialized = false;
        int messageId = 1;
        int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public const string NotificationTypeKey = "type";

        NotificationManagerCompat compatManager;
        public static NotificationManagerService Instance { get; private set; }
        #endregion

        #region Constructor
        public NotificationManagerService()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                compatManager = NotificationManagerCompat.From(Platform.AppContext);
                Instance = this;
            }
        }
        #endregion

        #region Public
        public void SendNotification(string title, string message,NotificationType notificationType,DateTime? notifyTime = null)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            if (notifyTime != null)
            {
                Intent intent = new Intent(Platform.AppContext, typeof(AlarmHandler));
                intent.PutExtra(TitleKey, title);
                intent.PutExtra(MessageKey, message);
                intent.PutExtra(NotificationTypeKey, (int)notificationType);
                intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

                var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                    ? PendingIntentFlags.CancelCurrent | PendingIntentFlags.Immutable
                    : PendingIntentFlags.CancelCurrent;

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);
                long triggerTime = GetNotifyTime(notifyTime.Value);
                AlarmManager alarmManager = Platform.AppContext.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            }
            else
            {
                Show(title, message,notificationType);
            }
        }

        public void Show(string title, string message,NotificationType notificationType)
        {
           
            Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

            var pendingIntentFlags = (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
                : PendingIntentFlags.UpdateCurrent;

            PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);

            //Base Part
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, ChannelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetOnlyAlertOnce(true)
                .SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.dotnet_logo))
                .SetSmallIcon(Resource.Drawable.message_small);

            if (notificationType == NotificationType.ProgressBarNotification)
            {
                builder.SetProgress(100, 0, false);
                builder.SetOngoing(true);
                builder.SetAutoCancel(false);
                builder.SetPriority(NotificationCompat.PriorityHigh);
            }
            Notification notification = builder.Build();
            messageId = Random.Shared.Next();
            compatManager.Notify(messageId, notification);

            if (notificationType == NotificationType.ProgressBarNotification)
            {
                SimulateProgressBar(builder,messageId);
            }
           
        }

        private async void SimulateProgressBar(NotificationCompat.Builder builder,int messageId)
        {
            int progress = 0;
            while (progress <= 100)
            {
                await Task.Delay(1000);
                progress += 5;
                builder.SetProgress(100, progress, false);
                compatManager.Notify(messageId, builder.Build());
            }

            builder.SetProgress(0, 0, false)
            .SetContentText("Download complete"); // Update the message

            compatManager.Notify(messageId, builder.Build());
        }
        #endregion

        #region Private
        void CreateNotificationChannel()
        {
            // Create the notification channel, but only on API 26+.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(ChannelName);
                var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = ChannelDescription
                };
                // Register the channel
                NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
                manager.CreateNotificationChannel(channel);
                channelInitialized = true;
            }
        }

        long GetNotifyTime(DateTime notifyTime)
        {
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTime; // milliseconds
        }
        #endregion
    }
}
