
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Media;
using Android.Net;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    public class PlayAudio : IPlayAudio
    {
		private MediaPlayer mediaPlayer;
        private List<TrackModel> trackModels;
        private Context context;
        private TrackModel currentTrack;
        private int currentTrackIndex = 0;

        public PlayAudio()
        {
            mediaPlayer = new MediaPlayer();
            context = Android.App.Application.Context;
            trackModels = new List<TrackModel>();

            trackModels = GetTracksFromRoot(GetMusicDirectory());
        }

        [System.Obsolete]
        public void StartPlayTrack()
		{
            //var directoryPath = GetMusicDirectory();
            //trackModels = GetTracksFromRoot(directoryPath);
            currentTrack = trackModels[0];
            Uri uri = Uri.Parse(currentTrack.Path);

            SetPlayerSourse(uri);
        }

        private void StartPlayTrack(TrackModel model)
        {
            mediaPlayer = new MediaPlayer();
            Uri uri = Uri.Parse(model.Path);

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

        private string[] GetMusicDirectory()
		{
			string directoryDownloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            return Directory.GetFiles(directoryDownloadsPath);
        }

        private List<TrackModel> GetTracksFromRoot(string[] directoriesPathArray)
        {
            foreach (string musicFilePath in directoriesPathArray)
            {
                MediaMetadataRetriever mmr = new MediaMetadataRetriever();
                mmr.SetDataSource(musicFilePath);

                trackModels.Add(new TrackModel
                {
                    Album = mmr.ExtractMetadata(MetadataKey.Album),
                    Title = mmr.ExtractMetadata(MetadataKey.Title),
                    Artist = mmr.ExtractMetadata(MetadataKey.Artist),
                    Genre = mmr.ExtractMetadata(MetadataKey.Genre),
                    Path = musicFilePath
                });
            }

            return trackModels;
        }

        private void ResetPlayer()
        {
            mediaPlayer.Reset();
            mediaPlayer.Release();
            mediaPlayer = null;
        }

        private void SetPlayerSourse(Uri uri)
        {
            mediaPlayer.SetDataSource(context, uri);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }
    }
}