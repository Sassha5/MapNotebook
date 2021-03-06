﻿using System.Threading.Tasks;

namespace MapNotepad.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task<int> RegisterAsync(string name, string email, string password);
        Task<bool> CheckEmailTakenAsync(string email);
    }
}
