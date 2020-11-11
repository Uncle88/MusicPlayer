using System.Collections.Generic;
using MusicPlayer.Model;
using MusicPlayer.Services.PlayService;
using Xamarin.Forms;

namespace MusicPlayer.PageModels
{
    public class ListTabbedPageModel : BasePageModel
    {
        private IPlayAudio audioService;
        private IList<TrackModel> tracksList = new List<TrackModel>();

        public ListTabbedPageModel()
        {
            audioService = DependencyService.Get<IPlayAudio>();
        }

        public override void Initialize()
        {
            base.Initialize();

            tracksList = audioService.GetTrackModelList();
        }
    }
}