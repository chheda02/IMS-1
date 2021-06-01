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

                //var str = $"{{\"BookRequestId\":\"{bookRequest.BookRequestId}\",\"RequestStatus\":\"{status}\",\"BookId\":\"{bookRequest.BookId}\",\"StudentId\":\"{bookRequest.StudentId}\"}}";
                //HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");

                //var responseTask = client.PutAsync("BookRequests/" + bookRequest.BookRequestId, content);
                var responseTask = client.PutAsJsonAsync("USERS/"+user.Id,user);
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
    }
}