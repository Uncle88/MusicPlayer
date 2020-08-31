
namespace MusicPlayer.Services.PlayService
{
    public interface IPlayAudio
    {
        void StartPlayTrack(string fileName);
        void ContinuePlayTrack();
        void PauseTrack();
    }
}