
using Xamarin.Forms;

namespace MusicPlayer.Services.PlayService
{
    public class PlayAudio : IPlayAudio
    {
        public void PlayFile(string fileName)
        {
            DependencyService.Get<IPlayAudio>().PlayFile(fileName);
        }
    }
}
