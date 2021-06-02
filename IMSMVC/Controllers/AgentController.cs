using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}