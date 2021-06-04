using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PressAgency.Models;

namespace PressAgency.Controllers
{
    [Authorize(Roles = "admin,Admin")]
    public class AdminsController : Controller
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: Admins
        public ActionResult adminPage()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                return View();
            }
        }


        public ActionResult Show_users()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                return View(db.Users.ToList());
            }

        }


        public ActionResult Show_profile()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                var id = Session["id"];
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }


        }


        // GET: Default/Edit/5
        public ActionResult Edit_profile(int id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                User user = db.Users.Find(id);
                return View(user);
            }

        }


        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit_profile(int id, FormCollection collection)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                try
                {
                    // Retrieve existing dinner
                    User user = db.Users.Find(id);

                    // Update dinner with form posted values

                    user.FirstName = collection["FirstName"];
                    user.LastName = collection["LastName"];
                    user.UserName = collection["UserName"];
                    user.Password = collection["Password"];
                    user.PhoneNumber = collection["PhoneNumber"];
                    user.photo = collection["photo"];
                    user.Email = collection["Email"];
                    // Persist changes back to database
                    db.SaveChanges();

                    return RedirectToAction("Show_profile");
                }
                catch
                {
                    return View();
                }
            }

        }

        public ActionResult Details_users(int? id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }

        }


        // GET: Admins/Create
        public ActionResult Create_users()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                TempData["roles"] = new SelectList(db.Roles, "id", "RoleName");
                return View();
            }

        }


        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_users([Bind(Include = "id,FirstName,LastName,UserName,Password,PhoneNumber,Email,photo,role_id")] User user)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("adminPage");
                }

                return View(user);
            }


        }


        // GET: Admins/Delete/5
        public ActionResult Delete_users(int? id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View();
            }

        }



        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete_users")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_users(int id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Show_users");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }











        public ActionResult Show_posts()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                return View(db.Posts.ToList());
            }

        }

        // GET: Admins/Details/5
        public ActionResult Create_posts(int? id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Post post = db.Posts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }

        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                return View();
            }
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_posts([Bind(Include = "id,FirstName,LastName,UserName,Password,PhoneNumber,Email,photo,Role")] Post post)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(post);
            }

        }

        // GET: Admins/Delete/5
        public ActionResult Delete_posts(int? id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Post post = db.Posts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }

        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete_posts")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_posts(int id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                Post post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Show_posts");
            }

        }

        public ActionResult Edit_posts(int? id)
        {
            if (Session["username"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "accounts");
            }
            else
            {
                return View();
            }
        }

    }
}
