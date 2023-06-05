using ExamenC_Web2.Services;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenC_Web2.Services
{
    public interface IExternalRepository
    {
        (string, AuthenticationProperties) ExternalLogin(string redirectUrl);
        Task<IdentityRepositoryResult> ExternalResponse();
    }
}
