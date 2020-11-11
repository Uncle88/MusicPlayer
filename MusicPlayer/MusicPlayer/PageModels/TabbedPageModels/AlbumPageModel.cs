
using System.Collections.Generic;
using FreshMvvm;
using MusicPlayer.Model;

namespace MusicPlayer.PageModels.TabbedPageModels
{
    public class AlbumPageModel : FreshBasePageModel
    {
        public List<TrackModel> TrackList { get; set; } = new List<TrackModel>();

        public AlbumPageModel()
        {
            var list = new List<TrackModel>
            {
                new TrackModel
                {
                    Title = "Title1"
                },
                new TrackModel
                {
                    Title = "Title2"
                },
                new TrackModel
                {
                    Title = "Title3"
                }
            };

            TrackList.AddRange(list);
        }
    }
}

