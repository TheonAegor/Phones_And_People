using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Phones_And_People.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext() : base("PeopleDb")
        { 
            Database.SetInitializer<PersonContext>(new PersonInitializer());
        }
        public DbSet<Person> People { get; set; }


    }
}