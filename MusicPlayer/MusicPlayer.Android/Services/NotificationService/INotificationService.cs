using System;
using Android.App;

namespace MusicPlayer.Droid.Services.NotificationService
{
    public interface INotificationService
    {
        Notification CreateNotification();
    }
}