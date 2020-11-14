
using System;
using Xamarin.Forms;

namespace MusicPlayer.Pages
{
    public partial class ListTabbedPage : BasePage
    {
        private readonly Color ActiveTabColor = Color.FromHex("#fffb660c");
        private readonly Color DefaultTabTextColor = Color.FromHex("#ff4b4b4b");

        public ListTabbedPage()
        {
            InitializeComponent();
            _artistButton.Clicked += _recoverButton_Clicked;
            _albumButton.Clicked += _temporaryButton_Clicked;
            _genresButton.Clicked += _programsButton_Clicked;
        }

        private void _programsButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _genresBox.BackgroundColor = ActiveTabColor;
            _genresButton.TextColor = ActiveTabColor;
        }

        private void _temporaryButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _albumBox.BackgroundColor = ActiveTabColor;
            _albumButton.TextColor = ActiveTabColor;
        }

        private void _recoverButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _artistBox.BackgroundColor = ActiveTabColor;
            _artistButton.TextColor = ActiveTabColor;
        }

        private void ClearStates()
        {
            _albumBox.BackgroundColor = Color.White;
            _genresBox.BackgroundColor = Color.White;
            _artistBox.BackgroundColor = Color.White;

            _albumButton.TextColor = DefaultTabTextColor;
            _genresButton.TextColor = DefaultTabTextColor;
            _artistButton.TextColor = DefaultTabTextColor;
        }
    }
}
