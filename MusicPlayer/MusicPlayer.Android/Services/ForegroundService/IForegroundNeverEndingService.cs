using System;
namespace MusicPlayer.Droid.Services.ForegroundService
{
    public interface IForegroundNeverEndingService
    {
        void StartService();
        void StopService();
    }
}
