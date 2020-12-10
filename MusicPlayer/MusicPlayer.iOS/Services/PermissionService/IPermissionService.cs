using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace MusicPlayer.iOS.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<PermissionStatus> GetPermissionStatus();
        Task<bool> RequestPermissionAsync();
    }
}
