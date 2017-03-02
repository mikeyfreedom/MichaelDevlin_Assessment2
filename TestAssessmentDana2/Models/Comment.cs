using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssessmentDana2.Models
{
    public class Comment
    {
        public Comment()
        {
            CommentDate = DateTime.Now.Date;
            CommentTime = DateTime.Now.TimeOfDay;
        }
        [Key]
        public int CommentID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CommentDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:%h}:{0:%m}:{0:%s}")]
        public TimeSpan CommentTime { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}
