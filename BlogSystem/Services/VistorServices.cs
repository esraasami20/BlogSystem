using BlogSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class VistorServices
    {
        BlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public VistorServices(BlogContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public void AddVistorId(string id)
        {
            _db.Vistors.Add(new Vistor() { VistorId = id });
            _db.SaveChanges();
        }
    }
}
