
using System.Collections.Generic;
using AVFoundation;
using Foundation;
using MediaPlayer;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.iOS.Services.PlayAudio))]
namespace MusicPlayer.iOS.Services
{
    public class PlayAudio : IPlayAudio
    {
        private AVAudioPlayer _mediaPlayer;

        public void StartPlayTrack()
        {
            var tracks = GetTrackList();
            _mediaPlayer = AVAudioPlayer.FromUrl(tracks[0]);
            _mediaPlayer.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
            {
                _mediaPlayer = null;
            };
            _mediaPlayer?.Play();
        }

        public void PauseTrack()
        {
            _mediaPlayer?.Pause();
        }

        public void ContinuePlayTrack()
        {
            StartPlayTrack();
        }

        private List<NSUrl> GetTrackList()
        {
            var list = new List<NSUrl>();
            var soundsQuery = MPMediaQuery.SongsQuery;
            var playlistCollection = soundsQuery.Collections;
            foreach (MPMediaItemCollection collection in playlistCollection)
            {
                list.Add(collection.Items[0].AssetURL);
            }

            return list;
        }

        public void PrevPlayTrack()
        {
        }

        public TrackModel GetCurrentTrackModel()
        {
            return new TrackModel();
        }

        public void NextPlayTrack()
        {
        }

        public List<TrackModel> GetTrackModelList()
        {
            return new List<TrackModel>();
        }

        public void StartPlayTrack(TrackModel model)
        {
            
        }

        public void StopTrack()
        {
            _mediaPlayer?.Stop();
        }

        public void SetVolume(bool isUp)
        {
            if (isUp)
            {
                _mediaPlayer?.SetVolume(0, 0);
            }
            else
            {
                _mediaPlayer?.SetVolume(1, 1);
            }
        }
    }
}