using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phones_And_People.Models;

namespace Phones_And_People.Controllers
{
    public class HomeController : Controller
    {
        PersonContext db = new PersonContext();
        public ActionResult Index(PersonViewModel pvm = null, int page = 0)
        {
            if ((pvm.People == null || pvm.PageInfo == null) && page == 0) {
                pvm = MakeViewModel();
                var partialView = PartialView("nextPage", pvm);
                return View("Index", partialView);
            }
            else if (page != 0)
            {
                pvm = MakeViewModel(page);
                return PartialView("nextPage");
            }
            return View(pvm);
        }
        public RedirectToRouteResult AddUsers()
        {
            PersonInitializer.FillDb(db);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public PartialViewResult nextPage(int page)
        {
            PersonViewModel pvm = MakeViewModel(page);
            return PartialView(pvm);
        }
        private PersonViewModel MakeViewModel(int page = 1)
        {
            PersonViewModel pvm = new PersonViewModel();
            List<Person> people = new List<Person>();
            IEnumerable<Person> people1;

            people = db.People.ToList();
            PageInfo pageInfo = new PageInfo { ItemsPerPage = 10, Page = page, TotalItems = people.Count() };

            people1 = people
                .Skip((page - 1) * pageInfo.ItemsPerPage)
                .Take(pageInfo.ItemsPerPage).ToList();

            pvm.PageInfo = pageInfo;
            pvm.People = people1;
            return pvm;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}