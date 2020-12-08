using System;
using System.Globalization;
using Xamarin.Forms;

namespace MusicPlayer.Converters
{
    public class VolumeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "volumeDisable.png" : "volumeEnable.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
