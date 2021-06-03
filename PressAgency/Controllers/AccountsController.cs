using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PressAgency.Models;
using System.Web.Security;

namespace PressAgency.Controllers
{
    public class AccountsController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();

        [AllowAnonymous]
        public ActionResult Login()
        {

            if (Session["username"] == null && Session["password"] == null)
            {
                return View();
            }
            else
            {
                if ((int)Session["id"] == 1)
                {
                    return RedirectToAction("adminPage", "Admins");
                }
                else if ((int)Session["id"] == 2)
                {
                    return RedirectToAction("editorPage", "Editors");
                }
                else
                {
                    return RedirectToAction("viewerPage", "Viewers");
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User model, string returnURL)
        {
            if (Session["username"] == null && Session["password"] == null)
            {
                ProjectDBEntities db = new ProjectDBEntities();
                var dataItem = db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password).First();
                if (dataItem != null)
                {
                    Session["username"] = dataItem.UserName;
                    Session["password"] = dataItem.Password;
                    Session["id"] = dataItem.id;
                    FormsAuthentication.SetAuthCookie(dataItem.UserName, false);
                    if (Url.IsLocalUrl(returnURL) && returnURL.Length > 1 && returnURL.StartsWith("/") && !returnURL.StartsWith("//") && !returnURL.StartsWith("/\\"))
                    {
                        if (dataItem.role_id == 1)
                        {
                            return RedirectToAction("adminPage", "Admins");
                        }
                        else if (dataItem.role_id == 2)
                        {
                            return RedirectToAction("EditorHome", "Editors");
                        }
                        else
                        {
                            return RedirectToAction("viewerPage", "Viewers");
                        }

                    }
                    else
                    {
                        Session["username"] = dataItem.UserName;
                        Session["password"] = dataItem.Password;
                        Session["id"] = dataItem.id;
                        if (dataItem.role_id == 1)
                        {
                            return RedirectToAction("adminPage", "Admins");
                        }
                        else if (dataItem.role_id == 2)
                        {
                            return RedirectToAction("EditorHome", "Editors");
                        }
                        else
                        {
                            return RedirectToAction("viewerPage", "Viewers");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User/Pass");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("adminPage", "admins");
            }

        }

        [Authorize]
        public ActionResult SignOut()
        {
            Session["username"] = null;
            Session["password"] = null;
            Session["id"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Signup()
        {
            TempData["roles"] = new SelectList(db.Roles, "id", "RoleName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signup(User model)
        {

            db.Users.Add(model);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}