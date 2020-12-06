
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
        public ICommand OpenTabbedPageCommand => new Command(OpenTabbedPageCommandExecute);
        public ICommand VolumeCommand => new Command(VolumeCommandExecute);

        public bool IsVolume { get; set; }
        public bool IsPlaying { get; set; }

        public StartPlayerPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        public override void ReverseInit(object returnedData)
        {
            if (returnedData is TrackModel model)
            {
                if (IsPlaying)
                {
                    IsPlaying = false;
                    audioService.StopTrack();
                }

                audioService.StartPlayTrack(model);
                SelectedMusic = model;
                StartTrackTimer(SelectedMusic);
                IsPlaying = true;
            }
        }

        public string Duration { get; set; }

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

        private int position;
        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
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
                    isFirstStart = true;
                    StartTrackTimer(SelectedMusic);
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
            Position = 0;
            if (isFirstStart)
            {
                IsPlaying = false;
                audioService.NextPlayTrack();
                SelectedMusic = audioService.GetCurrentTrackModel();
                StartTrackTimer(SelectedMusic);
                IsPlaying = true;
            }
        }

        private void PreviousTrackCommandExecute()
        {
            Position = 0;
            if (isFirstStart)
            {
                IsPlaying = false;
                audioService.PrevPlayTrack();
                SelectedMusic = audioService.GetCurrentTrackModel();
                StartTrackTimer(SelectedMusic);
                IsPlaying = true;
            }
        }

        private void StartTrackTimer(TrackModel selectedMusic)
        {
            Duration = SecondsToMinutes(selectedMusic.Duration);
            Position = TimeSpan.Parse(selectedMusic.Duration).Seconds;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                return true;
            });
        }

        public static string SecondsToMinutes(string duration)
        {
            int millSecond = int.Parse(duration);
            int hours, minutes, seconds = millSecond / 1000;

            hours = (seconds / 3600);
            minutes = (seconds / 60) % 60;
            seconds = seconds % 60;

            if (hours == 0)
            {
                return string.Format("{0, 0:d2}:{1, 0:d2}", minutes, seconds);
            }
            return string.Format("{0, 0:d2}:{1, 0:d2}:{2, 0:d2}", hours, minutes, seconds);
        }

        private async void OpenTabbedPageCommandExecute()
        {
            await CoreMethods.PushPageModel<ListTabbedPageModel>();
        }

        private void VolumeCommandExecute()
        {
            if (IsVolume)
            {
                audioService.SetVolume(false);
            }
            else
            {
                audioService.SetVolume(true);
            }

            IsVolume = !IsVolume;
        }
    }
}