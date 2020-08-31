
using Android.Media;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    public class PlayAudio : IPlayAudio
    {
		private MediaPlayer mediaPlayer;

        public void StartPlayTrack(string fileName)
		{
			mediaPlayer = new MediaPlayer();
			var fd = Android.App.Application.Context.Assets.OpenFd(fileName);
			mediaPlayer.Prepared += (s, e) =>
			{
				mediaPlayer?.Start();
			};
			mediaPlayer.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
			mediaPlayer.Prepare();
		}

		public void ContinuePlayTrack()
		{
			mediaPlayer?.Start();
		}

		public void PauseTrack()
		{
			mediaPlayer?.Pause(); 
		}
	}
}