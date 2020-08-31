
using Xamarin.Forms;

namespace MusicPlayer.Services.PlayService
{
    public class PlayAudio : IPlayAudio
    {
        public void StartPlayTrack(string fileName)
        {
            DependencyService.Get<IPlayAudio>().StartPlayTrack(fileName);
        }

        public void PauseTrack()
        {
            DependencyService.Get<IPlayAudio>().PauseTrack();
        }

        public void ContinuePlayTrack()
        {
            DependencyService.Get<IPlayAudio>().ContinuePlayTrack();
        }
    }
}