
using System.Collections.Generic;
using MusicPlayer.Model;

namespace MusicPlayer.Services.PlayService
{
    public interface IPlayAudio
    {
        void StartPlayTrack();
        void StartPlayTrack(TrackModel model);
        void ContinuePlayTrack();
        void PauseTrack();

        void NextPlayTrack();
        void PrevPlayTrack();

        TrackModel GetCurrentTrackModel();
        List<TrackModel> GetTrackModelList();
    }
}