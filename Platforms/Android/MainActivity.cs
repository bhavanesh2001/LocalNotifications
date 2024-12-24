using Android.App;
using Android.Content;
using Android.Content.PM;
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
            CreateNotificationChannel();
#pragma warning restore CA1416
        }

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
        private void CreateNotificationChannel()
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {

                NotificationChannel channel = new NotificationChannel(
                    AndroidConstants.STATUS_BAR_CHANNEL_ID,
                    new Java.Lang.String(AndroidConstants.STATUS_BAR_CHANNEL_NAME), NotificationImportance.Default);
                channel.Description = AndroidConstants.STATUS_BAR_CHANNEL_DESCRIPTION;
                if(GetSystemService(Context.NotificationService) is NotificationManager notificationManager)
                notificationManager?.CreateNotificationChannel(channel);
            }
        }

        public void SendNotification()
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Android.App.Application.Context, AndroidConstants.STATUS_BAR_CHANNEL_ID)
        
        .SetContentTitle("Test")
        .SetContentText("This is a test notification")
        .SetSmallIcon(Resource.Mipmap.appicon_foreground)
        .SetPriority(NotificationCompat.PriorityDefault);
       
            var manager = NotificationManagerCompat.From(Android.App.Application.Context);
            manager.Notify(Random.Shared.Next(1,5000),builder.Build());
        }
    }
}
