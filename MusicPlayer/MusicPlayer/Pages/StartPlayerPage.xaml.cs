
using MusicPlayer.PageModels;
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
    }
}
