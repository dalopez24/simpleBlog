using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using simpleBlog.Contracts.Repositories;
using simpleBlog.Model;
using System.Data.Entity;

namespace simpleBlog.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class UsersController : Controller
    {
        private IBaseRepository<User> users;
        private IBaseRepository<Role> roles;

        public UsersController(IBaseRepository<User> users, IBaseRepository<Role> roles)
        {
            this.users = users;
            this.roles = roles;
        }


        // GET: Admin/Users
        public ActionResult Index()
        {
            var userList = users.GetAll().Include(r => r.Roles).ToList();
            return View(userList);
        }


        // GET: Admin/Users/Details/5
        public ActionResult Details(int id)
        {
            var user = users.GetById(id);
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(roles.GetAll(), "RoleId", "Name");
            var newuser = new User();
            return View(newuser);
        }

        // POST: Admin/Users/Create
        [HttpPost]
        public ActionResult Create(User model)
        {
            if (!ModelState.IsValid)
                return View();

            var email = users.GetAll().FirstOrDefault(x => x.Email == model.Email);

            if (email != null)
            {
                ModelState.AddModelError("Email", "Email Already exist");
                return View();
            }

            model.SetPassword(model.Password);

            users.Insert(model);
            users.Commit();

            return RedirectToAction("Index");
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Roles = new SelectList(roles.GetAll(), "RoleId", "Name");
            var getuser = users.GetById(id);
            return View(getuser);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
                return View();

            users.Update(user);
            users.Commit();

            return RedirectToAction("Index");

        }


        public ActionResult Delete(int id)
        {
            users.Delete(id);
            users.Commit();
            return RedirectToAction("Index");
        }



        public ActionResult AllRoles()
        {
            var roleList = roles.GetAll();
            return View(roleList);
        }

        public ActionResult Createrole()
        {
            var role = new Role();
            return View(role);
        }
        [HttpPost]
        public ActionResult Createrole(Role role)
        {
            roles.Insert(role);
            roles.Commit();
            return RedirectToAction("AllRoles");
        }

        public ActionResult DeleteRole(int id)
        {
            roles.Delete(id);
            roles.Commit();
            return RedirectToAction("AllRoles");
        }

    }


}

