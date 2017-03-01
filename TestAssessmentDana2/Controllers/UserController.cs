using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAssessmentDana2.Models
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var model = new ListUsersViewModel();
            model.UserRoleModels = new List<UserRoleModel>();
            
            foreach (var role in roles)
            {
                UserRoleModel ur = new UserRoleModel();
                ur.Role = role;
                ur.Users = new List<ApplicationUser>();
                foreach (var user in users)
                {
                    if (userManager.IsInRole(user.Id,role.Name))
                        ur.Users.Add(user);
                }
                if (ur.Users != null)
                    model.UserRoleModels.Add(ur);
            }

            return View(model);
       }

        [HttpPost]
        public ActionResult Promote()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            string uID = Request.Form["userID"];
            string roleName = Request.Form["roleName"];
            userManager.RemoveFromRole(uID, roleName);
            userManager.AddToRole(uID, RoleNames.ROLE_PROMOTEDUSER);

            return RedirectToAction("Index");
        }
    }
}