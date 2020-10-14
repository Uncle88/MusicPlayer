
using Android.App;
using Android.Content;

namespace MusicPlayer.Droid.Services
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "CLOSE" })]
    public class CustomActionReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;
            if (action != null)
            {
                var intentClose = new Intent(context, typeof(PlayAudio));
                context.StopService(intentClose);

                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
    }
}
