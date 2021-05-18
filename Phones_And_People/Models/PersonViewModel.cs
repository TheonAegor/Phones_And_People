using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phones_And_People.Models
{
    public class PersonViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<Person> People { get; set; }
        public DateViewModel DateViewModel { get; set; }
        

    }

    public class PageInfo
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages {
            get { return (int)Math.Ceiling((decimal)(TotalItems / ItemsPerPage)); }
        }
    }

    public class DateViewModel
    {
        public SelectList Years { get; set; }
        public SelectList Months { get; set; }
        public SelectList Days { get; set; }
    }
}