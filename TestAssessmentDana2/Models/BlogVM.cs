using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssessmentDana2.Models
{
    public class BlogVM
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
