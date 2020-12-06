
using FreshMvvm;
using MusicPlayer.PageModels;
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

            var page = FreshPageModelResolver.ResolvePageModel<StartPlayerPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        private void RegisterServices()
        {
            FreshIOC.Container.Register<IPlayAudio, PlayAudio>();
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
