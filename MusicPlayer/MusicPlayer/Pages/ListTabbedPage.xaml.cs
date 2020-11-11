
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
            _recoverButton.Clicked += _recoverButton_Clicked;
            _temporaryButton.Clicked += _temporaryButton_Clicked;
            _programsButton.Clicked += _programsButton_Clicked;
        }

        private void _programsButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _programsBox.BackgroundColor = ActiveTabColor;
            _programsButton.TextColor = ActiveTabColor;
        }

        private void _temporaryButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _temporaryBox.BackgroundColor = ActiveTabColor;
            _temporaryButton.TextColor = ActiveTabColor;
        }

        private void _recoverButton_Clicked(object sender, EventArgs e)
        {
            ClearStates();
            _recoverBox.BackgroundColor = ActiveTabColor;
            _recoverButton.TextColor = ActiveTabColor;
        }

        private void ClearStates()
        {
            _temporaryBox.BackgroundColor = Color.White;
            _programsBox.BackgroundColor = Color.White;
            _recoverBox.BackgroundColor = Color.White;

            _temporaryButton.TextColor = DefaultTabTextColor;
            _programsButton.TextColor = DefaultTabTextColor;
            _recoverButton.TextColor = DefaultTabTextColor;
        }
    }
}
