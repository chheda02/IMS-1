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
        public ActionResult BuyPolicies(BuyPolicies buypolicy)
        {
            buypolicy.UserId = (int)Session["UserId"];
            Policies policy = (Policies)Session["Policy"];
            buypolicy.PolicyId = policy.Id;
            buypolicy.PolicyCategoryId = policy.CategoryId;
            buypolicy.CreatedDate = DateTime.Now;
            buypolicy.UpdatedDate = DateTime.Now.AddMonths((int)policy.DurationInMonths);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("BuyPolicies", buypolicy);
                responseTask.Wait();

                var result = responseTask.Result;
            }
            PoliciesTransactions policiesTransactions = new PoliciesTransactions();
            policiesTransactions.UserId = (int)Session["UserId"];
            policiesTransactions.PolicyId = policy.Id;
            policiesTransactions.Amount = policy.PremiumAmount;
            policiesTransactions.ActualPaymentDate = DateTime.Now;
            policiesTransactions.ActualPremiumDate = DateTime.Now;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("PoliciesTransactions", policiesTransactions);
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
        public ActionResult ViewBuyPolicies()
        {
            IEnumerable<BuyPolicies> buypolicy = null;
            List<BuyPolicies> buypolicy1 = null;

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
                    buypolicy = readTask.Result;
                }
                else
                {
                    //Error response received   
                    buypolicy = Enumerable.Empty<BuyPolicies>();
                }
                BuyPolicies policies = new BuyPolicies();
                buypolicy1.Add(policies);
                foreach (var items in buypolicy)
                {
                    if(items.UserId==(int)Session["UserId"])
                    {
                        buypolicy1.Add(items);
                    }
                }
                return View(buypolicy1);
            }
        }
        [HttpGet]
        public ActionResult MakePoliciesClaim(int Id)
        {
            BuyPolicies buyPolicies = new BuyPolicies();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("BuyPolicies/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BuyPolicies>();
                    readTask.Wait();
                    buyPolicies = readTask.Result;
                }
            }
            Session["PoliciesClaim"] = buyPolicies;
            return View();
        }
        [HttpPost]
        public ActionResult MakePoliceiesClaim(PoliciesClaim policiesClaim)
        {
            policiesClaim.CreatedDate = DateTime.Now;
            BuyPolicies buyPolicies = (BuyPolicies)Session["PoliciesClaim"];
            policiesClaim.PolicyId = buyPolicies.PolicyId;
            policiesClaim.UserId = (int)Session["UserId"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("PoliciesClaims", policiesClaim);
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
        public ActionResult RenewPolicy(int Id)
        {
            BuyPolicies buyPolicies = new BuyPolicies();
            Session["buyPoliciesId"] = Id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("BuyPolicies/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BuyPolicies>();
                    readTask.Wait();
                    buyPolicies = readTask.Result;
                }
            }
            PoliciesTransactions policiesTransactions = new PoliciesTransactions();
            policiesTransactions.PolicyId = buyPolicies.PolicyId;
            policiesTransactions.Amount = buyPolicies.AmountPaid;
            policiesTransactions.ActualPremiumDate = buyPolicies.UpdatedDate;
            return View(policiesTransactions); 
        }
        [HttpPost]
        public ActionResult RenewPolicy(PoliciesTransactions policiesTransactions)
        {
            int buyPoliciesId = (int)Session["buyPoliciesId"];
            BuyPolicies buyPolicies = new BuyPolicies();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("BuyPolicies/" + buyPoliciesId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BuyPolicies>();
                    readTask.Wait();
                    buyPolicies = readTask.Result;
                }
            }
            Policies policy = new Policies();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("Policies/" + buyPolicies.PolicyId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Policies>();
                    readTask.Wait();
                    policy = readTask.Result;
                }
            }
            buyPolicies.UpdatedDate = DateTime.Now.AddMonths((int)policy.DurationInMonths);
            policiesTransactions.UserId = (int)Session["UserId"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("PoliciesTransactions", policiesTransactions);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Home", "Customer", new { Id = policiesTransactions.UserId });
                }
                else
                {
                    return View();
                }
            }
        }
    }
}