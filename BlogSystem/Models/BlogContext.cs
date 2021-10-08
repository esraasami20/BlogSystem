using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class BlogContext:IdentityDbContext<ApplicationUser>
    {


        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Moderator> Moderators { get; set; }
        public virtual DbSet<Vistor> Vistors { get; set; }
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Moderator>().ToTable("Moderator");
            builder.Entity<Vistor>().ToTable("Vistor");




            builder.Entity<Blog>()
           .Property(b => b.CreatedAt)
           .HasDefaultValueSql("getdate()");

            builder.Entity<Comment>()
          .Property(b => b.CreatedAt)
          .HasDefaultValueSql("getdate()");


        }
        public BlogContext(DbContextOptions options) : base(options)
        {
        }
    }
}
