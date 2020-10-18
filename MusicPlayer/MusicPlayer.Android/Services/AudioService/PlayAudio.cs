
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using MusicPlayer.Droid.Models.TrackModel;
using MusicPlayer.Droid.Services.NotificationService;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    [Service]
    public class PlayAudio : MediaBrowserServiceCompat, IPlayAudio
    {
        private const int ServiceRunningNotifID = 9000;
        private const string MediaSessionTag = "TAG";

        private MediaPlayer mediaPlayer;
        private List<TrackModel> trackModels;
        private Context context;
        private TrackModel currentTrackPlaying;
        private INotificationService notificationService;

        private MediaSessionCompat mediaSession;
        private PlaybackStateCompat.Builder stateBuilder;

        public override void OnCreate()
        {
            base.OnCreate();

            // Create a MediaSessionCompat
            mediaSession = new MediaSessionCompat(context, MediaSessionTag);

            // Enable callbacks from MediaButtons and TransportControls
            mediaSession.SetFlags(MediaSessionCompat.FlagHandlesMediaButtons | MediaSessionCompat.FlagHandlesTransportControls);

            // Set an initial PlaybackState with ACTION_PLAY, so media buttons can start the player
            stateBuilder = new PlaybackStateCompat.Builder()
                .SetActions(PlaybackStateCompat.ActionPlay | PlaybackStateCompat.ActionPlayPause);
            mediaSession.SetPlaybackState(stateBuilder.Build());

            // MySessionCallback() has methods that handle callbacks from a media controller
            //mediaSession.SetCallback(new MySessionCallback());

            // Set the session's token so that client activities can communicate with it.
            SessionToken = mediaSession.SessionToken;
        }

        public override BrowserRoot OnGetRoot(string clientPackageName, int clientUid, Bundle rootHints)
        {
            return null;
        }

        public override void OnLoadChildren(string parentId, Result result)
        {

        }

        public PlayAudio()
        {
            mediaPlayer = new MediaPlayer();
            context = Application.Context;
            trackModels = new List<TrackModel>();

            notificationService = new NotificationService.NotificationService();
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
			string directory = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, Environment.DirectoryDownloads);

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

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notif = notificationService.CreateNotification();
            StartForeground(ServiceRunningNotifID, notif);

            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent currentIntent)
        {
            return base.StopService(currentIntent);
        }
    }
}