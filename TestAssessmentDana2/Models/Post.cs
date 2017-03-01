using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssessmentDana2.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        [DisplayName("Title")]
        public string PostTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:%h}:{0:%m}:{0:%s}")]
        public TimeSpan PostTime { get; set; }
        [DataType(DataType.MultilineText)] 
        public string Content { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            PostDate = DateTime.Now.Date;
            PostTime = DateTime.Now.TimeOfDay;
        }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
