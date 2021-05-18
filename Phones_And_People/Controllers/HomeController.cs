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
        public ActionResult Index( string SearchString, string sortOrder, string CurrentFilter, int? y, int? m, int? d, int page = 1)
        {
            PersonViewModel pvm;
            List<Person> people = new List<Person>();
            DateViewModel dateViewModel = new DateViewModel();

            people = db.People.ToList();
            ViewBag.CurrYearFilt = y;
            ViewBag.CurrMonthFilt = m;
            ViewBag.CurrDayFilt = d;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = sortOrder == "TDesc" ? "TAsc" : "TDesc";
            ViewBag.FnameSort = sortOrder == "FAsc" ? "FDesc" : "FAsc";
            ViewBag.LnameSort = sortOrder == "LDesc" ? "LAsc" : "LDesc";
            ViewBag.Date = sortOrder == "DDesc" ? "DAsc" : "DDesc";

            if (SearchString != null)
                page = 1;
            else
                SearchString = CurrentFilter;

            ViewBag.CurrentFilter = SearchString;

            if (!string.IsNullOrEmpty(SearchString))
            {
                people = people.Where(p => p.Title.Contains(SearchString) || p.FirstName.Contains(SearchString) || p.LastName.Contains(SearchString)).ToList();
            }    

            //people = people1 as List<Person>;

            switch (sortOrder)
            {
                case "TDesc":
                    people = people.OrderByDescending(p => p.Title).ToList();
                    break;
                case "TAsc":
                    people = people.OrderBy(p => p.Title).ToList();
                    break;
                case "LDesc":
                    people = people.OrderByDescending(p => p.LastName).ToList();
                    break;
                case "LAsc":
                    people = people.OrderBy(p => p.LastName).ToList();
                    break;
                case "DDesc":
                    people = people.OrderByDescending(p => p.DoB).ToList();
                    break;
                case "DAsc":
                    people = people.OrderBy(p => p.DoB).ToList();
                    break;
                case "FDesc":
                    people = people.OrderByDescending(p => p.FirstName).ToList();
                    break;
                default:
                    people = people.OrderBy(p => p.FirstName).ToList();
                    break;
            }

                if (y != null && y != 0)
            {
                people = people.Where(p => p.DoB.Year == y).ToList();
            }
            if (m != null && m != 0)
                people = people.Where(p => p.DoB.Month == m).ToList();
            if (d != null && d != 0)
                people = people.Where(p => p.DoB.Year == d).ToList();

            
            dateViewModel.Years = new SelectList((from p in people
                                                  orderby p.DoB.Year
                                                 select p.DoB.Year).Distinct());
            dateViewModel.Months = new SelectList((from p in people
                                                   orderby p.DoB.Month
                                                  select p.DoB.Month).Distinct());
            dateViewModel.Days = new SelectList((from p in people
                                                 orderby p.DoB.Day
                                                select p.DoB.Day).Distinct());

            pvm = MakeViewModel(people, dateViewModel, page);
            return View(pvm); 
        }
        public RedirectToRouteResult AddUsers()
        {
            PersonInitializer.FillDb(db);
            return RedirectToAction("Index");
        }
        private PersonViewModel MakeViewModel(List<Person> newDb, DateViewModel dateViewModel, int page = 1)
        {
            PersonViewModel pvm = new PersonViewModel();
            List<Person> people = new List<Person>();
            IEnumerable<Person> people1;

            people = newDb;
            if (people == null)
            {
                pvm.PageInfo = new PageInfo { ItemsPerPage = 10, Page = page, TotalItems = 0 };
                pvm.People = people;
                pvm.DateViewModel = dateViewModel;
                return pvm;
            }
            PageInfo pageInfo = new PageInfo { ItemsPerPage = 10, Page = page, TotalItems = people.Count() };

            people1 = people
                .Skip((page - 1) * pageInfo.ItemsPerPage)
                .Take(pageInfo.ItemsPerPage).ToList();

            pvm.PageInfo = pageInfo;
            pvm.People = people1;
            pvm.DateViewModel = dateViewModel;
            return pvm;
        }
        public ActionResult About()
        {
            ViewBag.Message = "История создания!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Все понравилось и хотите предложить мне работу разработчиком, но не знаете как связаться со мной?";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}