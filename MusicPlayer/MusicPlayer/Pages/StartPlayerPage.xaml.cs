
using Xamarin.Forms;

namespace MusicPlayer.Pages
{
    public partial class StartPlayerPage : ContentPage
    {
        public StartPlayerPage()
        {
            InitializeComponent();

            double totalTime = 50000;
            double startTime = 300;
            var animation = new Animation(v => _bar.Progress = v, startTime / totalTime, 1, Easing.Linear);
            animation.Commit(this, "LinearProgressFromTime", 16, (uint)(totalTime - startTime), Easing.Linear, (v, c) => _bar.Progress = 1, () => false);
        }
    }
}
