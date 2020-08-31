
using FreshMvvm;
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;

namespace MusicPlayer.PageModels
{
    public class StartPlayerPageModel : FreshBasePageModel
    {
        private Command _playStopCommand;
        private IPlayAudio audioService;
        private bool _isTrackPlay = false;
        private bool _isFirstStart = false;

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        public Command PlayStopCommand
        {
            get
            {
                return _playStopCommand ?? (_playStopCommand = new Command(() =>
                {
                    if (!_isTrackPlay)
                    {
                        if (!_isFirstStart)
                        {
                            audioService.StartPlayTrack("stas.mp3");
                            _isFirstStart = true;
                        }
                        else
                        {
                            audioService.ContinuePlayTrack();
                        }
                        _isTrackPlay = true;
                    }
                    else
                    {
                        audioService.PauseTrack();
                        _isTrackPlay = false;
                    }
                }));
            }
        }
    }
}