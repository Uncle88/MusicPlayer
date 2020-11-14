using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;

namespace MusicPlayer.PageModels
{
    public class ListTabbedPageModel : BasePageModel
    {
        private IPlayAudio audioService;
        private List<TrackModel> tracksList = new List<TrackModel>();

        public List<TrackModel> Items { get; set; }

        public ICommand SelectedArtistTabCommand => new Command(SelectedArtistTabCommandExecute);
        public ICommand SelectedAlbumTabCommand => new Command(SelectedAlbumTabCommandExecute);
        public ICommand SelectedGenreTabCommand => new Command(SelectedGenreTabCommandExecute);

        public ListTabbedPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
            tracksList = audioService.GetTrackModelList();

            Items = tracksList.OrderBy(x => x.Artist).ToList();
        }

        private void SelectedArtistTabCommandExecute()
        {
            Items = tracksList.OrderBy(x => x.Artist).ToList();
        }

        private void SelectedAlbumTabCommandExecute()
        {
            Items = tracksList.OrderBy(x => x.Album).ToList();
        }

        private void SelectedGenreTabCommandExecute()
        {
            Items = tracksList.OrderBy(x => x.Genre).ToList();
        }
    }
}