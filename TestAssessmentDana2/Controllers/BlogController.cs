using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAssessmentDana2.Models;

namespace TestAssessmentDana2.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            var posts = db.Posts.ToList();
            var comments = db.Comments.ToList();
            var users = db.Users.ToList();
            BlogVM model = new BlogVM();
            model.Posts = posts;
            model.Comments = comments;
            model.Users = users;

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName");
            var post = new Post();
            post.PostDate = DateTime.Now.Date;
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,PostTitle,PostDate,PostTime,Content,UserID,CategoryID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "FirstName", post.UserID);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentID,CommentDate,CommentTime,Content,PostID")] Comment comment)
        {
            try
            {
                string formValue = Request.Form["Content"];
                string postID = Request.Form["postID"];
                int pID = Int32.Parse(postID);
                comment.Content = formValue;
                comment.PostID = pID;
                comment.Author = User.Identity.Name;

                if (ModelState.IsValid)
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            

            ViewBag.PostID = new SelectList(db.Posts, "PostID", "PostTitle", comment.PostID);
            return View(comment);
        }
    }
}