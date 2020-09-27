
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
            Page page = FreshPageModelResolver.ResolvePageModel<StartPlayerPageModel>();
            MainPage = page;
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
