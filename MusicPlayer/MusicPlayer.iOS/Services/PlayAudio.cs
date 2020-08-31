
using System.IO;
using AVFoundation;
using Foundation;
using MusicPlayer.Services.PlayService;


[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer.iOS.Services.PlayAudio))]
namespace MusicPlayer.iOS.Services
{
    public class PlayAudio : IPlayAudio
    {
        private AVAudioPlayer _mediaPlayer;

        public void StartPlayTrack(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);
            _mediaPlayer = AVAudioPlayer.FromUrl(url);
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
    }
}