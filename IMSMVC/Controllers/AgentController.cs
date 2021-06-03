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
    }
}