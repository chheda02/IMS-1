using IMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IMSMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Home()
        {
            IEnumerable<Policies> policy = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("Policies");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Policies>>();
                    readTask.Wait();
                    policy = readTask.Result;
                }
                else
                {
                    //Error response received   
                    policy = Enumerable.Empty<Policies>();
                }

                return View(policy);
            }
        }
        [HttpGet]
        public ActionResult BuyPolicies()
        {
            Policies policy = new Policies();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("Policies/" + policy.Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Policies>();
                    readTask.Wait();
                    policy = readTask.Result;
                }
            }
            return View(policy);
        }
        [HttpPost]
        public ActionResult BuyPolicies(Policies policy)
        {
            return View();
        }
    }
}