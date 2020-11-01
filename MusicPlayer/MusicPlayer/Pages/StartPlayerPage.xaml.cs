
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
            if (playImage.IsPlaying || nextImage.IsPlaying || prevImage.IsPlaying)
            {
                await image.RotateTo(0, 0);
                await image.RotateTo(360, image.Duration, Easing.BounceIn);
            }
            else
            {
                await image.RotateTo(0, 0);
            }
        }
    }
}
