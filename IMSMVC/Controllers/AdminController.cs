using IMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IMSMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Home()
        {
            IEnumerable<BuyPolicies> policy = null;

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
                    policy = readTask.Result;
                }
                else
                {
                    //Error response received   
                    policy = Enumerable.Empty<BuyPolicies>();
                }

                return View(policy);
            }
        }
        public ActionResult ViewUsers()
        {
            IEnumerable<User> users = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("USERS");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<User>>();
                    readTask.Wait();
                    users = readTask.Result;
                }
                else
                {
                    //Error response received   
                    users = Enumerable.Empty<User>();
                }

                return View(users);
            }
        }
        [HttpGet]
        public ActionResult EditUsers(int Id)
        {
            User users = new User();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("USERS/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    users = readTask.Result;
                }
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult EditUsers(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.PutAsJsonAsync("USERS/" + user.Id, user);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewUsers", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult DeleteUsers(int Id)
        {
            User users = new User();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("USERS/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    users = readTask.Result;
                }
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult DeleteUsers(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.DeleteAsync("USERS/" + user.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    return RedirectToAction("ViewUsers");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult DetailsUsers(int Id)
        {
            User users = new User();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("USERS/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    users = readTask.Result;
                }
            }
            return View(users);
        }
        public ActionResult ViewPolicies()
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
        public ActionResult EditPolicies(int Id)
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
            return View(policy);
        }
        [HttpPost]
        public ActionResult EditPolicies(Policies policy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.PutAsJsonAsync("Policies/" + policy.Id, policy);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewPolicies", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult CreatePolicies()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePolicies(Policies policy)
        {
            DateTime currentDateTime = DateTime.Now;
            policy.CreatedDate = currentDateTime;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.PostAsJsonAsync("Policies", policy);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewPolicies", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult DeletePolicies(int Id)
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
            return View(policy);
        }
        [HttpPost]
        public ActionResult DeletePolicies(Policies policy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.DeleteAsync("Policies/" + policy.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Policies>();
                    readTask.Wait();

                    return RedirectToAction("ViewPolicies");
                }
                else
                {
                    return View();
                }
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
            return View(policy);
        }
        public ActionResult ViewFeedbacks()
        {
            IEnumerable<Feedback> feedbacks = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("Feedbacks");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Feedback>>();
                    readTask.Wait();
                    feedbacks = readTask.Result;
                }
                else
                {
                    //Error response received   
                    feedbacks = Enumerable.Empty<Feedback>();
                }

                return View(feedbacks);
            }
        }
        [HttpGet]
        public ActionResult DeleteFeedbacks(int Id)
        {
            Feedback feedback = new Feedback();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("Feedbacks/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Feedback>();
                    readTask.Wait();
                    feedback = readTask.Result;
                }
            }
            return View(feedback);
        }
        [HttpPost]
        public ActionResult DeleteFeedbacks(Feedback feedback)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.DeleteAsync("Feedbacks/" + feedback.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Feedback>();
                    readTask.Wait();

                    return RedirectToAction("ViewFeedbacks");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult ViewComplaints()
        {
            IEnumerable<Complaint> complaints = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.GetAsync("Complaints");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Complaint>>();
                    readTask.Wait();
                    complaints = readTask.Result;
                }
                else
                {
                    //Error response received   
                    complaints = Enumerable.Empty<Complaint>();
                }

                return View(complaints);
            }
        }
        [HttpGet]
        public ActionResult DeleteComplaints(int Id)
        {
            Complaint complaint = new Complaint();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("Complaints/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Complaint>();
                    readTask.Wait();
                    complaint = readTask.Result;
                }
            }
            return View(complaint);
        }
        [HttpPost]
        public ActionResult DeleteComplaints(Complaint complaint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.DeleteAsync("Complaints/" + complaint.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Complaint>();
                    readTask.Wait();

                    return RedirectToAction("ViewComplaints");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult ViewBuyPolicies()
        {
            IEnumerable<BuyPolicies> buyPolicies = null;

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

                return View(buyPolicies);
            }
        }
        public ActionResult ViewPoliciesClaim()
        {
            IEnumerable<PoliciesClaim> policiesClaims = null;

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

                return View(policiesClaims);
            }
        }
        public ActionResult ViewPoliciesTransactions()
        {
            IEnumerable<PoliciesTransactions> policiesTransactions = null;

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

                return View(policiesTransactions);
            }
        }
        public ActionResult ViewCustomerAgents()
        {
            IEnumerable<CustomerAgent> customerAgents = null;

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

                return View(customerAgents);
            }
        }
        [HttpGet]
        public ActionResult EditCustomerAgents(int Id)
        {
            CustomerAgent customerAgent = new CustomerAgent();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("CustomerAgents/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAgent>();
                    readTask.Wait();
                    customerAgent = readTask.Result;
                }
            }
            return View(customerAgent);
        }
        [HttpPost]
        public ActionResult EditCustomerAgents(CustomerAgent customerAgent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.PutAsJsonAsync("CustomerAgents/" + customerAgent.Id, customerAgent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewCustomerAgents", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult DeleteCustomerAgents(int Id)
        {
            CustomerAgent customerAgent = new CustomerAgent();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("CustomerAgents/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAgent>();
                    readTask.Wait();
                    customerAgent = readTask.Result;
                }
            }
            return View(customerAgent);
        }
        [HttpPost]
        public ActionResult DeleteCustomerAgents(CustomerAgent customerAgent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask = client.DeleteAsync("CustomerAgents/" + customerAgent.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAgent>();
                    readTask.Wait();

                    return RedirectToAction("ViewCustomerAgents");
                }
                else
                {
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult DetailsCustomerAgents(int Id)
        {
            CustomerAgent customerAgent = new CustomerAgent();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("CustomerAgents/" + Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAgent>();
                    readTask.Wait();
                    customerAgent = readTask.Result;
                }
            }
            return View(customerAgent);
        }

    }
}
