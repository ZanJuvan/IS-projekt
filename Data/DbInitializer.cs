using BeeOrganizer.Data;
using BeeOrganizer.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BeeOrganizer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Cebelarstvo context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator", NormalizedName="ADMINISTRATOR"},
                new IdentityRole{Id="2", Name="Drustvenik", NormalizedName="DRUSTVENIK"}
            };
            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();
        }
    }
}