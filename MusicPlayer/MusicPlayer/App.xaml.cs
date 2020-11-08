
using MusicPlayer.Pages;
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;

namespace MusicPlayer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterServices();
            MainPage = new NavigationPage(new StartPlayerPage());
        }

        private void RegisterServices()
        {
            DependencyService.Register<IPlayAudio, PlayAudio>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
