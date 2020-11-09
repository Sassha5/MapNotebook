﻿using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MapNotepad.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        public PermissionService()
        {
        }

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
    }
}