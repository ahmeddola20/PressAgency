using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PressAgency.Models;

namespace PressAgency.Controllers
{
    public class EditorsController : Controller
    {

        ProjectDBEntities db = new ProjectDBEntities ();
        // GET: Editor
        public ActionResult EditorHome()
        {

            return View(db.Users.ToList());
        }
        public ActionResult Show_profile()
        {
            var id = Session["id"];
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        // GET: Editor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Edit_profile(int? id)
        {

            return View();

        }
       
        // GET: Editor/Create
        public ActionResult Create_new_post()
        {
            return View();
        }

        public ActionResult Show_MyPosts()
        {
            return View(db.Posts.ToList());
        }
        
        
        
        public ActionResult RecevedQuestions()
        {
            return View(db.Posts.ToList());
        }


        // POST: Editor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Editor/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Editor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Editor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Editor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }



        }

        public ActionResult change_password(int? id)
        {

            return View();

        }


    }
}
