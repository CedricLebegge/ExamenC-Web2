using ExamenC_Web2.Services;
using ExamenC_Web2.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenC_Web2.Services
{
    public class IdentityRepository : IIdentityRepository
    {
        UserManager<IdentityUser> _userManger;
        SignInManager<IdentityUser> _siginManager;

        public IdentityRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManger)
        {
            _userManger = userManager;
            _siginManager = signinManger;
        }


        public async Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password)
        {
            var result = await GetIdentityUserAsync(username, email, password);
            if (result.Succeeded)
                result = await LoginAsync(result.IdentityUser, password);

            return result;
        }

        private async Task<IdentityRepositoryResult> LoginAsync(IdentityUser user, string password)
        {
            var result = new IdentityRepositoryResult();
            var identityResult = await _siginManager.PasswordSignInAsync(user, password, false, false);
            if (identityResult.Succeeded)
                result.Succeeded = true;
            else
                result.AddError("Probleem met inloggen!");
            return result;
        }

        public async Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel registerData)
        {
            var result = new IdentityRepositoryResult();
            var identityUser = new IdentityUser { UserName = registerData.UserName, Email = registerData.Email };
            var identityResult = await _userManger.CreateAsync(identityUser, registerData.Password);
            if (identityResult.Succeeded)
            {
                result.Succeeded = true;
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    result.AddError(error.Description);
                }
            }
            return result;
        }

        private async Task<IdentityRepositoryResult> GetIdentityUserAsync(string username, string email, string password)
        {
            var result = new IdentityRepositoryResult();
            IdentityUser user = null;

            if (username != null)
            {
                user = await _userManger.FindByNameAsync(username);
                if (user == null)
                    result.AddError("Geen gebruiker gevonden met deze username!");
            }
            else
            {
                if (email != null)
                {
                    user = await _userManger.FindByEmailAsync(email);
                    if (user == null)
                        result.AddError("Geen gebruiker gevonden met dit emailadres!");
                }
            }

            if (user != null)
            {
                var identityResult = await _siginManager.PasswordSignInAsync(user, password, false, false);
                if (identityResult.Succeeded)
                {
                    result.Succeeded = true;
                    result.IdentityUser = user;
                }
                else
                {
                    result.AddError("Geen geldige login of wachtwoord.");
                }


            }

            return result;
        }

        public async Task SignOutAsync()
        {
            await _siginManager.SignOutAsync();
        }



    }
}
