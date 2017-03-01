using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssessmentDana2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //Populate Role Table
                if (!roleManager.RoleExists(RoleNames.ROLE_ADMINISTRATOR))
                {
                    var roleResult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMINISTRATOR));
                }

                if (!roleManager.RoleExists(RoleNames.ROLE_STANDARDUSER))
                {
                    var roleResult = roleManager.Create(new IdentityRole(RoleNames.ROLE_STANDARDUSER));
                }

                if (!roleManager.RoleExists(RoleNames.ROLE_STANDARDUSER))
                {
                    var roleResult = roleManager.Create(new IdentityRole(RoleNames.ROLE_STANDARDUSER));
                }

                string userName = "admin@admin.com";
                string password = "123456";

                //No need to hash passwords using this initializer method.
                //If using migration strategy, use PasswordHasher to hash the password.

                //Create Admin user and role
                var user = userManager.FindByName(userName);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FirstName = "Administrator",
                        LastName = "Admin",
                        PhoneNumber = "0141454637",
                        UserName = userName,
                        Email = userName,
                        EmailConfirmed = true                      
                        
                    };
                    userManager.Create(newUser, password);
                    userManager.AddToRole(newUser.Id, RoleNames.ROLE_ADMINISTRATOR);
                }
            }

            base.Seed(context);
            context.SaveChanges();
        }
    }
}
