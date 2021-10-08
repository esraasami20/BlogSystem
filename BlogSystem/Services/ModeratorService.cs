using BlogSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class ModeratorService
    {
        BlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ModeratorService(BlogContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public void AddModeratorId(string id)
        {
            _db.Moderators.Add(new Moderator() { ModeratorId = id });
            _db.SaveChanges();
        }
    }
}
