﻿using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MapNotepad.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<PermissionStatus> CheckPermissionAsync<T>() where T : Permissions.BasePermission, new();
        Task<PermissionStatus> RequestPermissionAsync<T>() where T : Permissions.BasePermission, new();
    }
}
