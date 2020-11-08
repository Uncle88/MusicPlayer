
using FreshMvvm;
using MusicPlayer.PageModels.TabbedPageModels;

namespace MusicPlayer.PageModels
{
    public class ListTabbedPageModel : BasePageModel
    {
        public ListTabbedPageModel()
        {
            var tabbedNavigationContainer = new FreshTabbedNavigationContainer();
            tabbedNavigationContainer.AddTab<AlbumPageModel>("Album", "");
            tabbedNavigationContainer.AddTab<ArtistPageModel>("Artist", "");
            tabbedNavigationContainer.AddTab<GenrePageModel>("Genre", "");
            this.CurrentPage = tabbedNavigationContainer;
        }
    }
}

