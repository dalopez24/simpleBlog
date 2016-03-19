using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using simpleBlog.Contracts.Repositories;
using simpleBlog.Model;
using simpleBlog.WebUI.Infrastructure;
using simpleBlog.WebUI.viewModels;


namespace simpleBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private IBaseRepository<Post> _posts;

        public HomeController(IBaseRepository<Post> posts)
        {
            this._posts = posts;
        }


        public const int postPerPage = 5;
        public ActionResult Index(int page = 1)
        {
            
            var postCount = _posts.GetAll().Count();

            var currentPostPage = _posts.GetAll()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * postPerPage)
                .Take(postPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostPage, postCount, page, postPerPage)
            } );
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}