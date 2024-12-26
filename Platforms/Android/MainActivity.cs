using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using LocalNotifications.Platforms.Android;
using System.Runtime.Versioning;

namespace LocalNotifications
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {



        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
#pragma warning disable CA1416
            RequestNotificationsPermission();
            CreateNotificationChannels();
#pragma warning restore CA1416
        }

        /// <summary>
        /// Requests notification permission on Android 13+ (API level 33+) if not already granted.
        /// </summary>
        [SupportedOSPlatform("android33.0")]
        private void RequestNotificationsPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                if (CheckSelfPermission(Android.Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    RequestPermissions(new[] { Android.Manifest.Permission.PostNotifications }, 0);
                }
            }
        }

        [SupportedOSPlatform("android26.0")]
        private void CreateNotificationChannels()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                List<NotificationChannel> channels = new List<NotificationChannel>();

                #region Simple Notification Channel

                var simpleNotificationchannel = new NotificationChannel(AndroidConstants.SIMPLE_NOTIFICATION_CHANNEL_ID,
                                                new Java.Lang.String(AndroidConstants.SIMPLE_NOTIFICATION_CHANNEL_NAME),
                                                NotificationImportance.Default);
                simpleNotificationchannel.Description = AndroidConstants.SIMPLE_NOTIFICATION_CHANNEL_DESCRIPTION;
                channels.Add(simpleNotificationchannel);
                #endregion

                if (GetSystemService(Context.NotificationService) is NotificationManager notificationManager)
                {
                    foreach (var channel in channels)
                    {
                        notificationManager?.CreateNotificationChannel(channel);
                    }
                }

            }
        }

        public void SendNotification(NotificationModel notificationInfo)
        {
            switch (notificationInfo.DisplayType)
            {
                case NotificationType.SimpleNotification:
                    SendSimpleNotification(notificationInfo);
                    break;
            }




        }

        public void SendSimpleNotification(NotificationModel notificationInfo)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Android.App.Application.Context, AndroidConstants.SIMPLE_NOTIFICATION_CHANNEL_ID)

            .SetContentTitle(notificationInfo.Title)
            .SetContentText(notificationInfo.Description)
            .SetSmallIcon(Resource.Mipmap.appicon_foreground)
            .SetPriority(NotificationCompat.PriorityDefault)
            .SetLargeIcon(NotificationHelper.GetAppIconAsBitmap());


            var manager = NotificationManagerCompat.From(Android.App.Application.Context);
            manager.Notify(Random.Shared.Next(1, 5000), builder.Build());
        }
    }
}
