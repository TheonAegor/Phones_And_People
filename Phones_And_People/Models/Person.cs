using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;

namespace Phones_And_People.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string Phone { get; set; }
        public string Picture { get; set; }
        public string BigPicture { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }

}