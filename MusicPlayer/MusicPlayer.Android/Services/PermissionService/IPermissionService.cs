using System;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace MusicPlayer.Droid.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<PermissionStatus> GetPermissionStatus();
        Task<bool> RequestPermissionAsync();
        bool CheckBuildVersion();
    }
}
