
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

namespace MusicPlayer.Droid.Services.NotificationService
{
    public class NotificationHelper : INotificationHelper
    {
        private static string foregroundChannelId = "9001";
        private static Context context = global::Android.App.Application.Context;

        public Notification CreateNotification()
        {
            // Building intent
            var intent = new Intent(context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("MusicPlayer", "Song");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            // Building action intent
            var actionIntent = new Intent();
            actionIntent.SetAction("CLOSE");
            var pIntent = PendingIntent.GetBroadcast(context, 0, actionIntent, PendingIntentFlags.CancelCurrent);

            var notifBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle("Your Title")
                .SetContentText("Main Text Body")
                .SetSmallIcon(Resource.Drawable.notification_tile_bg)
                .SetOngoing(true)
                .AddAction(Resource.Drawable.abc_btn_radio_material, "close", pIntent)
                .SetContentIntent(pendingIntent);

            // Building notification channel
            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                var notifManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notifManager != null)
                {
                    notifBuilder.SetChannelId(foregroundChannelId);
                    notifManager.CreateNotificationChannel(notificationChannel);
                }
            }

            return notifBuilder.Build();
        }
    }

    [BroadcastReceiver]
    [IntentFilter(new string[] { "CLOSE" })]
    public class CustomActionReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, intent.Action, ToastLength.Long).Show();

            var extras = intent.Extras;
            if (extras != null && !extras.IsEmpty)
            {
                var manager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                var notificationId = extras.GetInt("NotificationIdKey", -1);
                if (notificationId != -1)
                {
                    manager.Cancel(notificationId);
                }
            }
        }
    }
}
