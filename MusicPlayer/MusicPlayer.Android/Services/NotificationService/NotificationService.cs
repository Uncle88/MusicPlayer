
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;

namespace MusicPlayer.Droid.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        public static string ForegroundChannelId = "9001";
        public static int NotificationId = 1;
        private static Context context = global::Android.App.Application.Context;

        public Notification CreateNotification()
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            var notifBuilder = new NotificationCompat.Builder(context, ForegroundChannelId)
                .SetContentTitle("Your Title")
                .SetContentText("Main Text Body")
                .SetSmallIcon(Resource.Drawable.notification_tile_bg)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .AddAction(Resource.Drawable.abc_btn_radio_material, "P", MakePendingIntent("P"))
                .AddAction(Resource.Drawable.abc_btn_radio_material, "P/P", MakePendingIntent("P/P"))
                .AddAction(Resource.Drawable.abc_btn_radio_material, "N", MakePendingIntent("N"))
                .AddAction(Resource.Drawable.abc_btn_radio_material, "close", MakePendingIntent("close"));

            // Building notification channel
            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(ForegroundChannelId, "Title", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                var notifManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notifManager != null)
                {
                    notifBuilder.SetChannelId(ForegroundChannelId);
                    notifManager.CreateNotificationChannel(notificationChannel);
                    notifManager.Notify(null, NotificationId, notifBuilder.Build());
                }
            }

            return notifBuilder.Build();
        }

        private PendingIntent MakePendingIntent(string actionName)
        {
            var cont = Application.Context;
            var intent = new Intent(cont, typeof(CustomActionReceiver));
            intent.SetAction(actionName);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(cont, 0, intent, 0);
            return pendingIntent;
        }
    }
}
