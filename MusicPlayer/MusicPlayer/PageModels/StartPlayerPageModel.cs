
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;
using PropertyChanged;
using System.Windows.Input;
using System;
using MusicPlayer.Model;

namespace MusicPlayer.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StartPlayerPageModel : BasePageModel
    {
        private IPlayAudio audioService;
        private bool isFirstStart = false;

        public ICommand PlayStopCommand => new Command(PlayStopCommandExecute);
        public ICommand NextTrackCommand => new Command(NextTrackCommandExecute);
        public ICommand PreviousTrackCommand => new Command(PreviousTrackCommandExecute);
        public bool IsPlaying { get; set; }

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        private TrackModel selectedMusic;
        public TrackModel SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        private uint duration;
        public uint Duration
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
                if (!isFirstStart)
                {
                    audioService.StartPlayTrack();
                    SelectedMusic = audioService.GetCurrentTrackModel();
                    Duration = 5000;
                    isFirstStart = true;

                    Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                    {
                        //Duration = new TimeSpan(0, 0, 5); //mediaInfo.Duration;
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
            if (isFirstStart)
            {
                IsPlaying = false;
                audioService.NextPlayTrack();
                SelectedMusic = audioService.GetCurrentTrackModel();
                IsPlaying = true;
            }
        }

        private void PreviousTrackCommandExecute()
        {
            if (isFirstStart)
            {
                IsPlaying = false;
                audioService.PrevPlayTrack();
                SelectedMusic = audioService.GetCurrentTrackModel();
                IsPlaying = true;
            }
        }
    }
}