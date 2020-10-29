
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
        public ICommand NextTrackCommand => new Command(NextTrackCommandExecute);
        public ICommand PreviousTrackCommand => new Command(PreviousTrackCommandExecute);
        public bool IsPlaying { get; set; }

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan position;
        public TimeSpan Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        double maximum = 100f;
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > 0)
                {
                    maximum = value;
                    OnPropertyChanged();
                }
            }
        }

        private void PlayStopCommandExecute(object obj)
        {
            if (!IsPlaying)
            {
                if (!_isFirstStart)
                {
                    audioService.StartPlayTrack();
                    _isFirstStart = true;

                    Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                    {
                        Duration = new TimeSpan(0, 0, 5); //mediaInfo.Duration;
                        Maximum = 0;//duration.TotalSeconds;
                        Position = new TimeSpan(0, 0, 0); //mediaInfo.Position;
                        return true;
                    });
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

        private void NextTrackCommandExecute()
        {
            audioService.NextPlayTrack();
        }

        private void PreviousTrackCommandExecute()
        {
            audioService.PrevPlayTrack();
        }
    }
}