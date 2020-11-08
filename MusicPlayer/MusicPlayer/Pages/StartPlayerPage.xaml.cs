
using System;
using System.Threading.Tasks;
using MusicPlayer.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPlayerPage : BasePage
    {
        public StartPlayerPage()
        {
            InitializeComponent();
            BindingContext = new StartPlayerPageModel();
        }

        async void ButtonTapped(System.Object sender, System.EventArgs e)
        {
            uint timeout = 3000;

            if (playImage.IsPlaying || nextImage.IsPlaying || prevImage.IsPlaying)
            {
                animatedLabel.TranslationX = 0;
                await image.RotateTo(0, 0);

                await Task.WhenAll(
                image.RotateTo(360, timeout, Easing.BounceIn),
                animatedLabel.TranslateTo(-50, 0, timeout, Easing.Linear),
                animatedLabel.TranslateTo(50, 0, timeout, Easing.Linear));
            }
            else
            {
                await image.RotateTo(0, 0);
                animatedLabel.TranslationX = 0;
            }
        }
    }
}
