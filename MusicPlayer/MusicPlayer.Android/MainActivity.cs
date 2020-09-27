
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using MusicPlayer.Droid.Services.ForegroundService;
using Android.Content;
using MusicPlayer.Services.PlayService;

namespace MusicPlayer.Droid
{
    [Activity(Label = "MusicPlayer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IForegroundNeverEndingService foregroundService = new ForegroundNeverEndingService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //foregroundService.StartService();
            StartService(new Intent(this, typeof(Services.PlayAudio)));

            LoadApplication(new App());
            DependencyService.Register<IPlayAudio, Services.PlayAudio>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //protected override void OnDestroy()
        //{
        //    foregroundService.StopService();
        //    base.OnDestroy();
        //}
    }
}