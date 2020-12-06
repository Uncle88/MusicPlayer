
using System.Collections.Generic;
using MusicPlayer.Model;
using Xamarin.Forms;

namespace MusicPlayer.Services.PlayService
{
    public class PlayAudio : IPlayAudio
    {
        public void StartPlayTrack()
        {
            DependencyService.Get<IPlayAudio>().StartPlayTrack();
        }

        public void StartPlayTrack(TrackModel model)
        {
            DependencyService.Get<IPlayAudio>().StartPlayTrack(model);
        }

        public void PauseTrack()
        {
            DependencyService.Get<IPlayAudio>().PauseTrack();
        }

        public void StopTrack()
        {
            DependencyService.Get<IPlayAudio>().StopTrack();
        }

        public void ContinuePlayTrack()
        {
            DependencyService.Get<IPlayAudio>().ContinuePlayTrack();
        }

        public void NextPlayTrack()
        {
            DependencyService.Get<IPlayAudio>().NextPlayTrack();
        }

        public void PrevPlayTrack()
        {
            DependencyService.Get<IPlayAudio>().PrevPlayTrack();
        }

        public TrackModel GetCurrentTrackModel()
        {
            return DependencyService.Get<IPlayAudio>().GetCurrentTrackModel();
        }

        public List<TrackModel> GetTrackModelList()
        {
            return DependencyService.Get<IPlayAudio>().GetTrackModelList();
        }

        public void SetVolume(bool level)
        {
            DependencyService.Get<IPlayAudio>().SetVolume(level);
        }
    }
}