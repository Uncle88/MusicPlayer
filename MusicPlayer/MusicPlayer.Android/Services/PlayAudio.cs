using System.Collections.Generic;
using Android.Content;
using Android.Media;
using MusicPlayer.Droid.Services.PermissionService;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;
using static MusicPlayer.Droid.Helpers.AndroidStorageHelper;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    public class PlayAudio : IPlayAudio
    {
        private readonly IPermissionService permissionService;
		private MediaPlayer mediaPlayer;
        private List<TrackModel> trackModels;
        private Context context;
        private TrackModel currentTrack;
        private int currentTrackIndex = 0;
        private bool isPermissionGranted;

        public PlayAudio()
        {
            mediaPlayer = new MediaPlayer();
            context = Android.App.Application.Context;
            trackModels = new List<TrackModel>();

            permissionService = new PermissionService.PermissionService();

            InitData();
        }

        private async void InitData()
        {
            if (permissionService.CheckBuildVersion())
            {
                trackModels = GetTracksFromRoot(GetAllFilesFromDirectories(GetDirectories()));
            }
            else
            {
                var status = await permissionService.GetPermissionStatus();

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    trackModels = GetTracksFromRoot(GetAllFilesFromDirectories(GetDirectories()));
                }
                else
                {
                    isPermissionGranted = await permissionService.RequestPermissionAsync();
                }
            }
        }

        [System.Obsolete]
        public void StartPlayTrack()
		{
            if (trackModels.Count == 0 && isPermissionGranted)
            {
                trackModels = GetTracksFromRoot(GetAllFilesFromDirectories(GetDirectories()));

                currentTrack = trackModels[0];
                Android.Net.Uri uri = Android.Net.Uri.Parse(currentTrack.Path);

                SetPlayerSourse(uri);
            }
            else
            {
                currentTrack = trackModels[0];
                Android.Net.Uri uri = Android.Net.Uri.Parse(currentTrack.Path);

                SetPlayerSourse(uri);
            }
        }

        public void StartPlayTrack(TrackModel model)
        {
            mediaPlayer = new MediaPlayer();
            currentTrack = model;
            Android.Net.Uri uri = Android.Net.Uri.Parse(model.Path);

            SetPlayerSourse(uri);
        }

        public void ContinuePlayTrack()
        {
            mediaPlayer?.Start();
        }

        public void PauseTrack()
        {
            mediaPlayer?.Pause();
        }

        public void StopTrack()
        {
            mediaPlayer?.Stop();
        }

        public int CurrentTrackProgressPosition()
        {
            return (int)(mediaPlayer?.CurrentPosition);
        }

        public void NextPlayTrack()
        {
            ResetPlayer();

            currentTrackIndex = trackModels.IndexOf(currentTrack) + 1;
            if (currentTrackIndex > trackModels.Count - 1)
            {
                currentTrackIndex = 0;
            }
            currentTrack = trackModels[currentTrackIndex];
            StartPlayTrack(currentTrack);
        }

        public void PrevPlayTrack()
        {
            ResetPlayer();

            currentTrackIndex = trackModels.IndexOf(currentTrack) - 1;
            if (currentTrackIndex < 0)
            {
                currentTrackIndex = trackModels.Count - 1;
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
            return trackModels;
        }

        private List<TrackModel> GetTracksFromRoot(string[] directoriesPathArray)
        {
            foreach (string musicFilePath in directoriesPathArray)
            {
                MediaMetadataRetriever mmr = new MediaMetadataRetriever();

                if (musicFilePath.EndsWith(".mp3"))
                {
                    mmr.SetDataSource(musicFilePath);

                    trackModels.Add(new TrackModel
                    {
                        Album = mmr.ExtractMetadata(MetadataKey.Album),
                        Title = mmr.ExtractMetadata(MetadataKey.Title),
                        Artist = mmr.ExtractMetadata(MetadataKey.Artist),
                        Genre = mmr.ExtractMetadata(MetadataKey.Genre),
                        Duration = mmr.ExtractMetadata(MetadataKey.Duration),
                        Path = musicFilePath
                    });
                }
            }

            return trackModels;
        }

        private void ResetPlayer()
        {
            mediaPlayer.Reset();
            mediaPlayer.Release();
            mediaPlayer = null;
        }

        private void SetPlayerSourse(Android.Net.Uri uri)
        {
            mediaPlayer.SetDataSource(context, uri);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }

        public void SetVolume(bool isUp)
        {
            if (isUp)
            {
                mediaPlayer?.SetVolume(0, 0);
            }
            else
            {
                mediaPlayer?.SetVolume(1, 1);
            }
        }
    }
}