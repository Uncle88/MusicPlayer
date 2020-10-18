
using Android.App;
using Android.Content;
using Android.Widget;

namespace MusicPlayer.Droid.Services
{
    [BroadcastReceiver]
    public class CustomActionReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            switch (action)
            {

                case "close":
                    StartCloseAction(context);
                    return;

                case "P/P":
                    StartPlayPauseAction(context, intent);
                    return;

                case "N":
                    StartNextAction(context, intent);
                    return;

                case "P":
                    StartPrevAction(context, intent);
                    return;
            }
        }

        private void StartPrevAction(Context context, Intent intent)
        {
            Toast.MakeText(context, intent.Action.ToString(), ToastLength.Long).Show();
        }

        private void StartNextAction(Context context, Intent intent)
        {
            Toast.MakeText(context, intent.Action.ToString(), ToastLength.Long).Show();
        }

        private void StartPlayPauseAction(Context context, Intent intent)
        {
            Toast.MakeText(context, intent.Action.ToString(), ToastLength.Long).Show();
        }

        private void StartCloseAction(Context context)
        {
            var intentClose = new Intent(context, typeof(PlayAudio));
            context.StopService(intentClose);

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
