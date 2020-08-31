
using System.IO;
using AVFoundation;
using Foundation;
using MusicPlayer.Services.PlayService;


[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.iOS.Services.PlayAudio))]
namespace MusicPlayer.iOS.Services
{
    public class PlayAudio : IPlayAudio
    {
        public PlayAudio()
        {
            ObjCRuntime.Class.ThrowOnInitFailure = true;
        }

        public void PlayFile(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);
            var _mediaPlayer = AVAudioPlayer.FromUrl(url);
            _mediaPlayer.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
            {
                _mediaPlayer = null;
            };
            _mediaPlayer.Play();
        }
    }
}