using IMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IMSMVC.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Home(int Id)
        {
            Session["UserId"] = Id;
            return View();
        }
        [HttpGet]
        public ActionResult GiveFeedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GiveFeedback(Feedback feedback)
        {
            feedback.CreatedDate = DateTime.Now; ;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("Feedbacks", feedback);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Home", "Customer", new { Id = (int)Session["UserId"] });
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult GiveComplaints()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GiveComplaints(Complaint complaints)
        {
            complaints.CreatedDate = DateTime.Now;
            complaints.UserId = (int)Session["UserId"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("Complaints", complaints);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Home", "Customer", new { Id = (int)Session["UserId"] });
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult ViewBuyPoliocies()
        {
            IEnumerable<CustomerAgent> customerAgents = null;
            int[] customerId = new int[customerAgents.Count()];

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("CustomerAgents");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerAgent>>();
                    readTask.Wait();
                    customerAgents = readTask.Result;
                }
                else
                {
                    //Error response received   
                    customerAgents = Enumerable.Empty<CustomerAgent>();
                }
            }
            int i = 0;
            foreach(CustomerAgent item in customerAgents)
            {
                if(item.AgentId == (int)Session["UserId"])
                {
                    customerId[i] = (int)item.UserId;
                    i++;
                }
            }
            IEnumerable<BuyPolicies> buyPolicies = null;
            List<BuyPolicies> buyPolicies1 = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("BuyPolicies");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BuyPolicies>>();
                    readTask.Wait();
                    buyPolicies = readTask.Result;
                }
                else
                {
                    //Error response received   
                    buyPolicies = Enumerable.Empty<BuyPolicies>();
                }
            }
            foreach(BuyPolicies item in buyPolicies)
            {
                for (int j = 0; j < customerId.Length; j++)
                {
                    if (item.UserId == customerId[j])
                    {
                        buyPolicies1.Add(item);
                    }
                }
            }
            return View(buyPolicies1);
        }

        [HttpGet]
        public ActionResult ViewPoliciesClaim()
        {
            IEnumerable<CustomerAgent> customerAgents = null;
            int[] customerId = new int[customerAgents.Count()];

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("CustomerAgents");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerAgent>>();
                    readTask.Wait();
                    customerAgents = readTask.Result;
                }
                else
                {
                    //Error response received   
                    customerAgents = Enumerable.Empty<CustomerAgent>();
                }
            }
            int i = 0;
            foreach (CustomerAgent item in customerAgents)
            {
                if (item.AgentId == (int)Session["UserId"])
                {
                    customerId[i] = (int)item.UserId;
                    i++;
                }
            }
            IEnumerable<PoliciesClaim> policiesClaims = null;
            List<PoliciesClaim> policiesClaims1 = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("PoliciesClaims");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PoliciesClaim>>();
                    readTask.Wait();
                    policiesClaims = readTask.Result;
                }
                else
                {
                    //Error response received   
                    policiesClaims = Enumerable.Empty<PoliciesClaim>();
                }
            }
            foreach (PoliciesClaim item in policiesClaims)
            {
                for (int j = 0; j < customerId.Length; j++)
                {
                    if (item.UserId == customerId[j])
                    {
                        policiesClaims1.Add(item);
                    }
                }
            }
            return View(policiesClaims1);
        }

        [HttpGet]
        public ActionResult ViewPoliciesTransactions()
        {
            IEnumerable<CustomerAgent> customerAgents = null;
            int[] customerId = new int[customerAgents.Count()];

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("CustomerAgents");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerAgent>>();
                    readTask.Wait();
                    customerAgents = readTask.Result;
                }
                else
                {
                    //Error response received   
                    customerAgents = Enumerable.Empty<CustomerAgent>();
                }
            }
            int i = 0;
            foreach (CustomerAgent item in customerAgents)
            {
                if (item.AgentId == (int)Session["UserId"])
                {
                    customerId[i] = (int)item.UserId;
                    i++;
                }
            }
            IEnumerable<PoliciesTransactions> policiesTransactions = null;
            List<PoliciesTransactions> policiesTransactions1 = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("PoliciesTransactions");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PoliciesTransactions>>();
                    readTask.Wait();
                    policiesTransactions = readTask.Result;
                }
                else
                {
                    //Error response received   
                    policiesTransactions = Enumerable.Empty<PoliciesTransactions>();
                }
            }
            foreach (PoliciesTransactions item in policiesTransactions)
            {
                for (int j = 0; j < customerId.Length; j++)
                {
                    if (item.UserId == customerId[j])
                    {
                        policiesTransactions1.Add(item);
                    }
                }
            }
            return View(policiesTransactions1);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}