using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssessmentDana2.Models
{
    public class UserRoleModel
    {
        public IdentityRole Role { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

    }
}
