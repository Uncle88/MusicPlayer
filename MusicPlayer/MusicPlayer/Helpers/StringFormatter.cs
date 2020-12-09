using System;
namespace MusicPlayer.Helpers
{
    public static class StringFormatter
    {
        public static string DurationFormat(string duration)
        {
            int millSecond = int.Parse(duration);
            int hours, minutes, seconds = millSecond / 1000;

            hours = (seconds / 3600);
            minutes = (seconds / 60) % 60;
            seconds = seconds % 60;

            if (hours == 0)
            {
                return string.Format("{0, 0:d2}:{1, 0:d2}", minutes, seconds);
            }
            return string.Format("{0, 0:d2}:{1, 0:d2}:{2, 0:d2}", hours, minutes, seconds);
        }
    }
}
