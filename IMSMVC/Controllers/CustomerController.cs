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
        public ActionResult Home(int Id)
        {
            Session["UserId"] = Id;
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
        public ActionResult DetailsPolicies(int Id)
        {
            Policies policy = new Policies();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("Policies/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Policies>();
                    readTask.Wait();
                    policy = readTask.Result;
                }
            }
            Session["Policy"] = policy;
            return View(policy);
        }
        [HttpGet]
        public ActionResult BuyPolicies(int Id)
        {
            return View();   
        }
        [HttpPost]
        public ActionResult BuyPolicies(BuyPolicies buypolicy)
        {
            buypolicy.UserId = (int)Session["UserId"];
            Policies policy = (Policies)Session["Policy"];
            buypolicy.PolicyId = policy.Id;
            buypolicy.PolicyCategoryId = policy.CategoryId;
            buypolicy.CreatedDate = DateTime.Now;
            buypolicy.UpdatedDate = DateTime.Now;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("BuyPolicies", buypolicy);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Home", "Customer", new { Id = buypolicy.UserId });
                }
                else
                {
                    return View();
                }
            }
        }
    }
}