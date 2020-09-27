
using Android.App;
using Android.Content;

namespace MusicPlayer.Droid.Services.ForegroundService
{
    internal class ForegroundNeverEndingService : IForegroundNeverEndingService
    {
        private static Context context = Application.Context;

        public void StartService()
        {
            var intent = new Intent(context, typeof(PlayAudio));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }

        public void StopService()
        {
            var intent = new Intent(context, typeof(PlayAudio));
            context.StopService(intent);
        }
    }
}
