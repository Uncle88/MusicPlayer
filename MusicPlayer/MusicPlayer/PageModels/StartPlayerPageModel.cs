using System;
using System.Windows.Input;
using FreshMvvm;
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;

namespace MusicPlayer.PageModels
{
    public class StartPlayerPageModel : FreshBasePageModel
    {
        private IPlayAudio audioService;

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        private Command _playStopCommand;
        public Command PlayStopCommand
        {
            get
            {
                return _playStopCommand ?? (_playStopCommand = new Command(() =>
                {
                    audioService.PlayFile("stas.mp3");
                }));
            }
        }
    }
}
