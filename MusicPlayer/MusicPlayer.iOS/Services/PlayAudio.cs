
using System.Collections.Generic;
using AVFoundation;
using MediaPlayer;
using MusicPlayer.iOS.Services.PermissionService;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.iOS.Services.PlayAudio))]
namespace MusicPlayer.iOS.Services
{
    public class PlayAudio : IPlayAudio
    {
        private AVAudioPlayer _mediaPlayer;
        private readonly IPermissionService permissionService;
        private bool isPermissionGranted;
        private List<TrackModel> trackModels;
        private TrackModel currentTrack;
        private int currentTrackIndex = 0;

        public PlayAudio()
        {
            trackModels = new List<TrackModel>();
            permissionService = new PermissionService.PermissionService();

            InitData();
        }

        private async void InitData()
        {
            var status = await permissionService.GetPermissionStatus();

            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                isPermissionGranted = await permissionService.RequestPermissionAsync();
            }
            else
            {
                isPermissionGranted = true;
            }
        }

        public void StartPlayTrack()
        {
            if (isPermissionGranted)
            {
                trackModels = GetTrackList();
                currentTrack = trackModels[0];
                _mediaPlayer = AVAudioPlayer.FromUrl(new Foundation.NSUrl(currentTrack.Url));
                _mediaPlayer.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
                {
                    _mediaPlayer = null;
                };
                _mediaPlayer?.Play();
            }
        }

        public void StartPlayTrack(TrackModel model)
        {
            currentTrack = model;
            _mediaPlayer = AVAudioPlayer.FromUrl(new Foundation.NSUrl(model.Url));
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
            _mediaPlayer?.Play();
        }

        private List<TrackModel> GetTrackList()
        {
            var soundsQuery = MPMediaQuery.SongsQuery;
            var playlistCollection = soundsQuery.Collections;

            if (trackModels.Count == 0)
            {
                foreach (MPMediaItemCollection collection in playlistCollection)
                {
                    trackModels.Add(new TrackModel
                    {
                        Title = collection.Items[0].Title,
                        Album = collection.Items[0].AlbumTitle,
                        Genre = collection.Items[0].Genre,
                        DurationSec = collection.Items[0].PlaybackDuration.ToString(),
                        Artist = collection.Items[0].Artist,
                        Url = collection.Items[0].AssetURL.ToString()
                    });
                }
            }

            return trackModels;
        }

        public void PrevPlayTrack()
        {
            _mediaPlayer?.Stop();
            currentTrackIndex = trackModels.IndexOf(currentTrack) - 1;
            if (currentTrackIndex < 0)
            {
                currentTrackIndex = trackModels.Count - 1;
            }
            currentTrack = trackModels[currentTrackIndex];
            StartPlayTrack(currentTrack);
        }
        
        public void NextPlayTrack()
        {
            _mediaPlayer?.Stop();
            currentTrackIndex = trackModels.IndexOf(currentTrack) + 1;
            if (currentTrackIndex > trackModels.Count - 1)
            {
                currentTrackIndex = 0;
            }
            currentTrack = trackModels[currentTrackIndex];
            StartPlayTrack(currentTrack);
        }

        public TrackModel GetCurrentTrackModel()
        {
            return currentTrack;
        }

        public List<TrackModel> GetTrackModelList()
        {
            return GetTrackList();
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

        public int CurrentTrackProgressPosition()
        {
            return (int)(_mediaPlayer?.CurrentTime);
        }
    }
}