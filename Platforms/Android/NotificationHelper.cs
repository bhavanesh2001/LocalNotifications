using Android.Graphics.Drawables;
using Android.Graphics;
using Application = Android.App.Application;

namespace LocalNotifications.Platforms.Android
{
    public static class NotificationHelper
    {
        public static Bitmap GetAppIconAsBitmap()
        {

            var packageManager = Application.Context.PackageManager;


            var appIconDrawable = packageManager.GetApplicationIcon(Application.Context.ApplicationInfo);


            if (appIconDrawable is BitmapDrawable bitmapDrawable)
            {
                return bitmapDrawable.Bitmap;
            }
            var bitmap = Bitmap.CreateBitmap(appIconDrawable.IntrinsicWidth, appIconDrawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            appIconDrawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            appIconDrawable.Draw(canvas);

            return bitmap;
        }
    }
}
