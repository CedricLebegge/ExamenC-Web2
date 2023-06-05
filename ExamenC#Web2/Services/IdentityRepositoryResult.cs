using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenC_Web2.Services
{
    public class IdentityRepositoryResult
    {
        List<string> _errors = new List<string>();

        public IEnumerable<string> Errors => _errors;

        public IdentityUser IdentityUser { get; set; }
        public bool Succeeded { get; set; }
        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
