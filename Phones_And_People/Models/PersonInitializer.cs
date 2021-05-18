using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using Phones_And_People.Models;

namespace Phones_And_People.Models
{
    
    public class PersonInitializer : DropCreateDatabaseIfModelChanges<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {
            FillDb(context);

            base.Seed(context);
        }
        static public void FillDb(PersonContext context)
        {
            int i = 0;
            Person pers = new Person();
            var client = new RestClient("https://randomuser.me/api/?results=100");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                while (i < content["results"].Count())
                {
                    var str = content["results"][i];
                    pers.Title = str["name"]["title"].Value<string>();
                    pers.FirstName = str["name"]["first"].Value<string>();
                    pers.LastName = str["name"]["last"].Value<string>();
                    pers.Phone = str["phone"].Value<string>();
                    pers.Email = str["email"].Value<string>();
                    pers.Picture = str["picture"]["medium"].Value<string>();
                    pers.BigPicture = str["picture"]["large"].Value<string>();
                    pers.DoB = str["dob"]["date"].Value<DateTime>();
                    pers.Password = str["login"]["password"].Value<string>();
                    context.People.Add(pers);
                    context.SaveChanges();
                    i++;
                }
            }
        }
    }
}