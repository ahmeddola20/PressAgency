using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PressAgency.Models;

namespace PressAgency.Controllers
{
    [Authorize(Roles ="admin,Admin")]
    public class AdminsController : Controller
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: Admins
        public ActionResult adminPage()
        {
            return View();
        }
        public ActionResult Show_users()
        {
            return View(db.Users.Find("id"));
        }

        public ActionResult Show_profile()
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

        public ActionResult Details_users(int? id)
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

        // GET: Admins/Create
        public ActionResult Create_users()
        {
            TempData["roles"] = new SelectList(db.Roles, "id", "RoleName");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_users([Bind(Include = "id,FirstName,LastName,UserName,Password,PhoneNumber,Email,photo,Role")] User user)
        {
            
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("adminPage");
            }

            return View(user);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete_users(int? id)
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

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete_users")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_users(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Show_users");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Edit_users(int? id) {
            return View();
        }

        public ActionResult Show_posts()
        {
            return View(db.Posts.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Create_posts(int? id)
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

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_posts([Bind(Include = "id,FirstName,LastName,UserName,Password,PhoneNumber,Email,photo,Role")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete_posts(int? id)
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

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete_posts")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_posts(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Show_posts");
        }

        public ActionResult Edit_posts(int? id)
        {
            return View();
        }

    }
}
