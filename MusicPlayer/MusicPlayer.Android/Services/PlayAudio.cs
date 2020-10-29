
using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Media;
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
        }

        [System.Obsolete]
        public void StartPlayTrack()
		{
            trackModels = GetTrackModelsList();
            currentTrack = trackModels[0];
            Android.Net.Uri uri = Android.Net.Uri.Parse(currentTrack.Path);

            mediaPlayer.SetDataSource(context, uri);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }

        public void ContinuePlayTrack()
		{
			mediaPlayer?.Start();
		}

		public void PauseTrack()
		{
			mediaPlayer?.Pause(); 
		}

        private void StartPlayTrack(TrackModel model)
        {
            mediaPlayer = new MediaPlayer();
            Android.Net.Uri uri = Android.Net.Uri.Parse(model.Path);

            mediaPlayer.SetDataSource(context, uri);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }

        public void NextPlayTrack()
        {
            ResetPlayer();

            currentTrackIndex = trackModels.IndexOf(currentTrack) + 1;
            currentTrack = trackModels[currentTrackIndex];
            StartPlayTrack(currentTrack);
        }

        public void PrevPlayTrack()
        {
            ResetPlayer();

            currentTrackIndex = trackModels.IndexOf(currentTrack) - 1;
            currentTrack = trackModels[currentTrackIndex];
            StartPlayTrack(currentTrack);
        }

        [System.Obsolete]
        public List<TrackModel> GetTrackModelsList()
		{
			string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

			var allFilesPaths = Directory.GetFiles(directory);
            foreach (string musicFilePath in allFilesPaths)
            {
                MediaMetadataRetriever mmr = new MediaMetadataRetriever();
                mmr.SetDataSource(musicFilePath);

                trackModels.Add(new TrackModel
                {
                    Album = mmr.ExtractMetadata(MetadataKey.Album),
                    Title = mmr.ExtractMetadata(MetadataKey.Title),
                    Artist = mmr.ExtractMetadata(MetadataKey.Artist),
                    Genre = mmr.ExtractMetadata(MetadataKey.Genre),
                    Image = mmr.ExtractMetadata(MetadataKey.ImagePrimary),
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
    }
}