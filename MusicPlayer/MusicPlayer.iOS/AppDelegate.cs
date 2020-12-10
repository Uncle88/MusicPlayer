
using Foundation;
using Lottie.Forms.iOS.Renderers;
using MusicPlayer.Services.PlayService;
using UIKit;
using Xamarin.Forms;

namespace MusicPlayer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIColor tintColor = UIColor.FromRGB(0, 153, 255);
            UINavigationBar.Appearance.BarTintColor = tintColor;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };
            UINavigationBar.Appearance.Translucent = false;

            AnimationViewRenderer.Init();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            DependencyService.Register<IPlayAudio, Services.PlayAudio>();

            return base.FinishedLaunching(app, options);
        }
    }
}
