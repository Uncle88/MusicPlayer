using System.Threading.Tasks;
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace MusicPlayer.Droid.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        public async Task<PermissionStatus> GetPermissionStatus()
        {
            return await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
        }

        public async Task<bool> RequestPermissionAsync()
        {
            var status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();

            switch (status)
            {
                case PermissionStatus.Denied:
                case PermissionStatus.Disabled:
                case PermissionStatus.Restricted:
                case PermissionStatus.Unknown:
                    return false;

                case PermissionStatus.Granted:
                    return true;
            }

            return false;
        }

        public bool CheckBuildVersion()
        {
            return (int)Build.VERSION.SdkInt < 23;
        }
    }
}
