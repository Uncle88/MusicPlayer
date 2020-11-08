using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer.Controls
{
    public class RotationImage : Image
    {
        public static BindableProperty DurationProperty = BindableProperty.Create(
                                                nameof(Duration),
                                                returnType: typeof(uint),
                                                declaringType: typeof(RotationImage),
                                                defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty IsPlayingProperty = BindableProperty.Create(
                                        nameof(IsPlaying),
                                        returnType: typeof(bool),
                                        declaringType: typeof(RotationImage),
                                        defaultValue: false);

        public uint Duration
        {
            get => (uint)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public bool IsPlaying
        {
            get => (bool)GetValue(IsPlayingProperty);
            set => SetValue(IsPlayingProperty, value);
        }
    }
}

