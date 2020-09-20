
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Content;
using Android.Media;
using Android.Net;
using MusicPlayer.Droid.Models.TrackModel;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    public class PlayAudio : IPlayAudio
    {
		private MediaPlayer mediaPlayer;
        private List<TrackModel> trackModels;
        private Context context;
        private TrackModel currentTrackPlaying;

        public PlayAudio()
        {
            mediaPlayer = new MediaPlayer();
            context = Android.App.Application.Context;
            trackModels = new List<TrackModel>();
        }

        public void StartPlayTrack()
		{
            trackModels = GetTrackModelsList();
            currentTrackPlaying = trackModels[0];
            Android.Net.Uri uri = Android.Net.Uri.Parse(trackModels[0].Path);

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

        public void NextPlayTrack()
        {
            mediaPlayer.Reset();
            mediaPlayer.Release();
            mediaPlayer = null;

            var nextTrack = currentTrackPlaying.Id + 1;
            var model = trackModels[nextTrack];
            StartPlayTrack(model);
        }

        private void StartPlayTrack(TrackModel currentTrackPlaying)
        {
            mediaPlayer = new MediaPlayer();
            Android.Net.Uri uri = Android.Net.Uri.Parse(currentTrackPlaying.Path);

            mediaPlayer.SetDataSource(context, uri);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }

        public void PrevPlayTrack()
        {
            if (mediaPlayer.IsPlaying)
            {
            }
        }

        private List<TrackModel> GetTrackModelsList()
		{
			string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

			var allFilesPaths = Directory.GetFiles(directory);
            foreach (string musicFilePath in allFilesPaths)
            {
                MediaMetadataRetriever mmr = new MediaMetadataRetriever();
                mmr.SetDataSource(musicFilePath);

                trackModels.Add(new TrackModel
                {
                    AlbumName = mmr.ExtractMetadata(MetadataKey.Album),
                    TrackName = mmr.ExtractMetadata(MetadataKey.Title),
                    ArtistName = mmr.ExtractMetadata(MetadataKey.Artist),
                    Genre = mmr.ExtractMetadata(MetadataKey.Genre),
                    Date = mmr.ExtractMetadata(MetadataKey.Date),
                    PhotoUrl = mmr.ExtractMetadata(MetadataKey.ImagePrimary),
                    Path = musicFilePath,
                    Id = allFilesPaths.ToList().IndexOf(musicFilePath)
                });
            }

            return trackModels;
        }
    }
}