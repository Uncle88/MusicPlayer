
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;
using PropertyChanged;
using System.Windows.Input;
using System;
using MusicPlayer.Model;
using MusicPlayer.Helpers;

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

        private string position;
        public string Position
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
            if (isFirstStart)
            {
                IsPlaying = false;
                audioService.PrevPlayTrack();
                SelectedMusic = audioService.GetCurrentTrackModel();
                StartTrackTimer(SelectedMusic);
                IsPlaying = true;
            }
        }

        private double progressValue;
        public double ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                OnPropertyChanged();
            }
        }
        private double maxProgressValue = 100;
        public double MaxProgressValue
        {
            get { return maxProgressValue; }
            set
            {
                maxProgressValue = value;
                OnPropertyChanged();
            }
        }

        private void StartTrackTimer(TrackModel selectedMusic)
        {
            if (selectedMusic.DurationSec == null)
            {
                Duration = StringFormatter.DurationFormat(selectedMusic.DurationMillisec);
                MaxProgressValue = Convert.ToDouble(selectedMusic.DurationMillisec);

                Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>
                {
                    ProgressValue = (double)audioService.CurrentTrackProgressPosition();
                    Position = StringFormatter.DurationFormat(audioService.CurrentTrackProgressPosition().ToString());

                return true;
                });
            }
            else
            {
                MaxProgressValue = Convert.ToDouble(selectedMusic.DurationSec);
                Duration = StringFormatter.DurationFormat((MaxProgressValue * 1000).ToString());

                Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>
                {
                    ProgressValue = (double)audioService.CurrentTrackProgressPosition();
                    Position = StringFormatter.DurationFormat((audioService.CurrentTrackProgressPosition()*1000).ToString());

                    return true;
                });
            }
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