using System;
using System.Globalization;
using Xamarin.Forms;

namespace MusicPlayer.Converters
{
    public class PlayPauseImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "pause.png" : "play.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}