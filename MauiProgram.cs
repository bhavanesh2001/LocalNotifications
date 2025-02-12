﻿using Microsoft.Extensions.Logging;

namespace LocalNotifications
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            #if ANDROID
            builder.Services.AddTransient<INotificationManagerService, LocalNotifications.Platforms.Android.NotificationManagerService>();
            #elif IOS
            builder.Services.AddTransient<INotificationManagerService, LocalNotifications.Platforms.iOS.NotificationManagerService>();
            #endif
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
