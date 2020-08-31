
using Android.Media;
using MusicPlayer.Services.PlayService;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.Droid.Services.PlayAudio))]
namespace MusicPlayer.Droid.Services
{
    public class PlayAudio : IPlayAudio
    {
        public PlayAudio()
        {
        }

		public void PlayFile(string fileName)
		{
			//var u = Android.Net.Uri.Parse(fileName);
			//MediaPlayer _player = MediaPlayer.Create(this, u);
			//_player.Start();

			//_player = MediaPlayer.Create(this, Resource.Raw.vMireZhivotnyh);
			//_player.Start();

			var player = new MediaPlayer();
			var fd = Android.App.Application.Context.Assets.OpenFd(fileName);
			player.Prepared += (s, e) =>
			{
				player.Start();
			};
			player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
			player.Prepare();
		}
    }
}
