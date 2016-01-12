using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecondHandBusinessPlatform.Models;

namespace SecondHandBusinessPlatform.Controllers
{
    public class UserController : Controller
    {
        private SecondHandBusinessPlatformDbContext db = new SecondHandBusinessPlatformDbContext();

        //
        // GET: /User/
        // GET: /User/Index

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /User/Details

        public ActionResult Details()
        {
            if(Session["UserId"] != null)
            {
                User user = db.Users.Find(Session["UserId"]);
                if (user == null)
                {
                    return RedirectToAction("Login", "User");
                }
                return View(user);
            }
            return RedirectToAction("Login", "User");
        }

        //
        // GET: /User/Login

        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                User user = db.Users.Find(Session["UserId"]);
                if (user == null)
                {
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (Session["UserId"] != null)
            {
                User user = db.Users.Find(Session["UserId"]);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var users = from u in db.Users
                        where u.UserName == username
                        select u;
            User current = users.First();
            if (current == null || current.IsPasswordValid(password) == false)
            {
                return View();
            }
            Session["UserId"] = current.UserId;
            Session["UserName"] = current.UserName;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /User/Register

        public ActionResult Register()
        {
            if (Session["UserId"] != null)
            {
                User user = db.Users.Find(Session["UserId"]);
                if (user == null)
                {
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //
        // POST: /User/Register

        [HttpPost]
        public ActionResult Register(User user)
        {
            //user.EncryptedPassword = user.Password;
            //Console.WriteLine(user.EncryptedPassword);
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        //
        // GET: /User/Edit

        public ActionResult Edit()
        {
            if (Session["UserId"] != null)
            {
                User user = db.Users.Find(Session["UserId"]);
                if (user == null)
                {
                    return RedirectToAction("Login", "User");
                }
                return View(user);
            }
            return RedirectToAction("Login", "User");
        }

        //
        // POST: /User/Edit

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (Session["UserId"] != null)
            {
                User current = db.Users.Find(Session["UserId"]);
                if (current == null)
                {
                    return RedirectToAction("Login", "User");
                }
                if (ModelState.IsValid)
                {
                    current.UserName = user.UserName;
                    current.Email = user.Email;
                    current.Tel = user.Tel;
                    current.StudentId = user.StudentId;
                    current.Age = user.Age;
                    current.Gender = user.Gender;
                    current.QQ = user.QQ;
                    db.Entry(current).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "User", current);
            }
            return RedirectToAction("Login", "User");
        }

        //
        // GET: /User/LogOut

        //[HttpPost, ActionName("Delete")]
        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}