using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestAssessmentDana2.Models;

namespace TestAssessmentDana2.Controllers
{
    public class UserRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: UserRole
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var models = new List<UserRoleModel>();
            foreach (var role in roleManager.Roles.ToList())
            {
                UserRoleModel ur = new UserRoleModel();
                ur.Role = role;

                models.Add(ur);
            }

            return View(models);
        }

        public async Task<ActionResult> Details(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var role = await roleManager.FindByIdAsync(id);

            var users = new List<ApplicationUser>();
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            UserRoleModel userRoles = new UserRoleModel();
            userRoles.Users = users;
            userRoles.Role = role;
            
            return View(userRoles);
        }
    }
}