using IMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IMSMVC.Controllers
{
    public class loginController : Controller
    {
        public void SendEmailRegistered(string EmailId, int UserId)
        {
            var fromEmail = new MailAddress("d643359@gmail.com", "IMS");
            var toEmail = new MailAddress(EmailId);
            var fromEmailPassword = "dummy@123"; // Replace with actual password

            string subject = "";
            string body = "";

            subject = "Successful Registration";
            body = "<br/><br/>We are excited to tell you that your IMS account is registered" +
                    "Your UserId is " + UserId +
                    " <br/><br/>";



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword),
                EnableSsl = true
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult Index()
        {
            User user1 = new User();
            user1 = (User)Session["user"];
            IEnumerable<User> users = null;

            using (var client1 = new HttpClient())
            {

                client1.BaseAddress = new Uri("http://localhost:54109/api/");

                var responseTask1 = client1.GetAsync("USERS");
                responseTask1.Wait();

                //To store result of web api response.   
                var result1 = responseTask1.Result;

                //If success received   
                if (result1.IsSuccessStatusCode)
                {
                    var readTask1 = result1.Content.ReadAsAsync<IList<User>>();
                    readTask1.Wait();
                    users = readTask1.Result;
                    int UserId = 0;
                    foreach (var item in users)
                    {
                        if (item.Name == user1.Name && item.Password == user1.Password && item.PhoneNumber == user1.PhoneNumber && item.Email == user1.Email && user1.Gender == item.Gender && item.Address == user1.Address)
                        {
                            UserId = item.Id;
                        }
                    }
                    SendEmailRegistered(user1.Email, UserId);
                }
            }

            return View();
        }
        public bool SendEmail(string EmailId, int otp)
        {
            var fromEmail = new MailAddress("d643359@gmail.com", "IMS");
            var toEmail = new MailAddress(EmailId);
            var fromEmailPassword = "dummy@123"; // Replace with actual password

            string subject = "";
            string body = "";

            subject = "Email Verification";
            body = "<br/><br/>We are excited to tell you that your IMS account needs email verifiction" +
                    "Your OTP is " + otp +
                    " <br/><br/>";



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword),
                EnableSsl = true
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
            return true;
        }
        // GET: login
        public ActionResult Verify()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Verify(verify verify)
        {
            User users = new User();
            DateTime currentDateTime = DateTime.Now;
            users = (User)Session["user"];
            users.CreatedDate = currentDateTime;
            if (verify.otp == Convert.ToInt32(Session["otp"]))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54109/api/");
                    var responseTask = client.PostAsJsonAsync("USERS", users);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Login", new { ac = "Success" });
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }
        // GET: login/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // POST: login/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.RoleId = 1;
            try
            {
                if (ModelState.IsValid)
                {
                    Random r = new Random();
                    int otp = r.Next(1000, 9999);
                    if (SendEmail(user.Email, otp))
                    {
                        Session["otp"] = otp;
                        Session["user"] = user;
                        return RedirectToAction("Verify");
                    }
                }
                // TODO: Add insert logic here
                return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            User users = new User();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54109/api/");
                var responseTask = client.GetAsync("USERS/" + login.Id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    users = readTask.Result;
                }
            }
            if (users.Password == login.Password)
            {
                if (users.RoleId == 1)
                {
                    return RedirectToAction("Home", "Customer", new { Id = users.Id });
                }
                else if (users.RoleId == 2)
                {
                    return RedirectToAction("Home", "Agent", new { Id = users.Id });
                }
                else if (users.RoleId == 3)
                {
                    return RedirectToAction("Home", "Admin", new { Id = users.Id });
                }
            }
            else
            {
                ViewBag.Message = "Invalid User credentials";
                return View();
            }
            return View();
        }
    }
}
