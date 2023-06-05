using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamenC_Web2.ViewModels;
using ExamenC_Web2.Models;

namespace ExamenC_Web2.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Op basis van model en de overeenstemmende databank bv.
        //public DbSet<Product> Products { get; set; }
    }
}
