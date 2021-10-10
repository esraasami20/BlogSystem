using BlogSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Helpers
{
    sealed public class ManageRoles
        {
            private readonly RoleManager<IdentityRole> roleManager;
            private readonly UserManager<ApplicationUser> userManager;


            public ManageRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
            {
                this.roleManager = roleManager;
                this.userManager = userManager;
            }



            // add to Vistor 

            public async Task<bool> AddToCustomerRole(ApplicationUser user)
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync("Vistor"))
                        await roleManager.CreateAsync(new IdentityRole("Vistor"));

                    if (await roleManager.RoleExistsAsync("Vistor"))
                    {
                        await userManager.AddToRoleAsync(user, "Vistor");

                    }
                    return true;

                } 
                catch (Exception e)
                {
                    return false;
                }
            }


            // add to  Moderator

            public async Task<bool> AddToEmployeeRole(ApplicationUser user)
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync("Moderator"))
                        await roleManager.CreateAsync(new IdentityRole("Moderator"));

                    if (await roleManager.RoleExistsAsync("Moderator"))
                    {
                        await userManager.AddToRoleAsync(user, "Moderator");

                    }
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }


            // add to  admin

            public async Task<bool> AddToSAdminRole(ApplicationUser user)
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync("Admin"))
                        await roleManager.CreateAsync(new IdentityRole("Admin"));

                    if (await roleManager.RoleExistsAsync("Admin"))
                    {
                        await userManager.AddToRoleAsync(user, "Admin");

                    }
                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }
    
}
