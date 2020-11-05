using Microsoft.AspNet.Identity;
using Peak_Performance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Peak_Performance.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeakPerformance db = new PeakPerformance();
        [Authorize]
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated && User.IsInRole("Athlete"))
            {
                string id = User.Identity.GetUserId();
                Athlete user = db.Athletes.FirstOrDefault(p => p.UserId == id);
                ViewBag.Age = (Int32.Parse(DateTime.Today.ToString("yyyyMMdd")) - Int32.Parse(user.DOB.ToString("yyyyMMdd"))) / 10000;
                return View("AthleteProfile", user);
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Coach"))
            {
                string id = User.Identity.GetUserId();
                Coach temp = db.Coaches.FirstOrDefault(p => p.UserId == id);
                CoachProfileViewModel coach = new CoachProfileViewModel(temp.CoachId);
                return View("CoachProfile", coach);
            }
            else
            {
                return View();
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We're Here to Help, Contact Us";

            return View();
        }
    }
}