using Phones_And_People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Phones_And_People.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Person user = null;
                using (PersonContext db1 = new PersonContext())
                {
                    user = db1.People.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    
                    return View("PersonalPage", user);
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
                
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = null;
                using (PersonContext db = new PersonContext())
                {
                    user = db.People.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (PersonContext db = new PersonContext())
                    {
                        db.People.Add(new Person { Email = model.Name, Password = model.Password, DoB = DateTime.Now, LastName = "Last Name", FirstName="First Name", Title = "Title" });

                        db.SaveChanges();

                        user = db.People.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Personal()
        {
            Person person;
            PersonContext personContext = new PersonContext();

            person = personContext.People.FirstOrDefault(p => p.Email == User.Identity.Name);
            if (person != null)
            {
                return View("PersonalPage", person);
            }
            else
                return View("Error");
        }
    }
}