using System.Threading.Tasks;
using MapNotepad.Services.PermissionService;
using Xamarin.Essentials;

namespace MapNotepad.Droid.Services
{
    public class PermissionService : IPermissionService
    {
        #region -- IPermisstionService Implementation --

        public Task<PermissionStatus> CheckPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public async Task<PermissionStatus> RequestPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            var status = await CheckPermissionAsync<T>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<T>();
            }
            return status;
        }

        #endregion
    }
}
