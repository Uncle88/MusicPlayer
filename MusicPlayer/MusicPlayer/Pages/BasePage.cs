using System;
using MusicPlayer.PageModels;
using Xamarin.Forms;

namespace MusicPlayer.Pages
{
    public class BasePage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null && BindingContext is BasePageModel)
            {
                ((BasePageModel)BindingContext).Initialize();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext != null && BindingContext is BasePageModel)
            {
                ((BasePageModel)BindingContext).Deinitialize();
            }
        }
    }
}
