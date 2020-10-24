
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;
using PropertyChanged;
using System.Windows.Input;
using System;

namespace MusicPlayer.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StartPlayerPageModel : BasePageModel
    {
        private IPlayAudio audioService;
        private bool _isFirstStart = false;

        public ICommand PlayStopCommand => new Command(PlayStopCommandExecute);
        public ICommand ChangeTrackCommand => new Command(ChangeTrackCommandExecute);
        public bool IsPlaying { get; set; }

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        private void PlayStopCommandExecute(object obj)
        {
            if (!IsPlaying)
            {
                if (!_isFirstStart)
                {
                    audioService.StartPlayTrack();
                    _isFirstStart = true;
                }
                else
                {
                    audioService.ContinuePlayTrack();
                }
                IsPlaying = true;
            }
            else
            {
                audioService.PauseTrack();
                IsPlaying = false;
            }
        }

        private void ChangeTrackCommandExecute(object obj)
        {
            if ((string)obj == "P")
                PreviousTrack();
            else if ((string)obj == "N")
                NextTrack();
        }

        private void NextTrack()
        {
            audioService.NextPlayTrack();
        }

        private void PreviousTrack()
        {
            audioService.PrevPlayTrack();
        }
    }
}