using ExamenC_Web2.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenC_Web2.Services
{
    public interface IIdentityRepository
    {
        Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password);
        Task SignOutAsync();
        Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel registerData);
    }
}
