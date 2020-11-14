using System.Collections.Generic;
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

        public ListTabbedPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
            Items = tracksList = audioService.GetTrackModelList();
        }

        public override void Initialize()
        {
            base.Initialize();

            
        }
    }
}