
using MusicPlayer.Model;

namespace MusicPlayer.Services.PlayService
{
    public interface IPlayAudio
    {
        void StartPlayTrack();
        void ContinuePlayTrack();
        void PauseTrack();

        void NextPlayTrack();
        void PrevPlayTrack();

        TrackModel GetCurrentTrackModel();
    }
}