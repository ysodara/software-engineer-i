using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW8.DAL;
using HW8.Models;

namespace HW8.Controllers
{
    
    public class HomeController : Controller
    {
        private RecordContext db = new RecordContext();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Athletes
        public ActionResult ShowAthletes()
        {
            
            return View();
        }
        public JsonResult ShowAll()
        {

            var data = db.Athletes.Select(x=>x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchJson(string studentName)
        {

            var data = db.TimeRecords.Where(x => x.Athlete.Name.Contains(studentName)).Select(p =>
            new { p.Location.Date, p.EventTitle, p.Location.MeetTitle, Time = Math.Round(p.Time, 2),p.Athlete.Name }).OrderBy(i => i.EventTitle).ThenBy(i => i.Date).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult StudentStat()
        {
            
            return View();
        }
        public JsonResult showTeam()
        {
            var data = db.Teams.Select(p => p.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult showStudent(string teamName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Teams.Where(x => x.Name.Contains(teamName)).Select(p => p.Athletes).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showStudentDistance(string name)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.TimeRecords.Where(x => x.Athlete.Name.Contains(name)).Select(p => 
            new { p.EventTitle, p.Athlete.Name }).OrderBy(x => x.EventTitle).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult studentRaceGraph(string name, string n)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.TimeRecords.Where(x => x.EventTitle == n && x.Athlete.Name.Contains(name)).Select(p => 
            new { p.EventTitle, p.Time, p.Location.Date }).OrderBy(x => x.EventTitle).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        

        // GET: Athletes/Create
        public ActionResult AddAthlete()
        {
            var list = db.Athletes.Select(x => x.Gender).Distinct();
            ViewBag.Gender = new SelectList(list);
            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAthlete([Bind(Include = "ID,Name,Gender,TeamID")] Athlete athlete)
        {
                if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "ID", "Name", athlete.TeamID);
            return View(athlete);
        }

        // GET: Locations/Create
        public ActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation([Bind(Include = "ID,MeetTitle,Date")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }
        //AddRaceTime

        // GET: TimeRecords/Create
        public ActionResult AddRaceTime()
        {
            //List<SelectListItem> list2 = null;

            var list = db.TimeRecords.Select( x => x.EventTitle  ).Distinct().ToList();
            
            ViewBag.EventTitle = new SelectList(list);


            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Name");
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "MeetTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRaceTime([Bind(Include = "ID,EventTitle,Time,EventGender,AthleteID,LocationID")] TimeRecord timeRecord)
        {
            if (ModelState.IsValid)
            {
                db.TimeRecords.Add(timeRecord);
                db.SaveChanges();
                return RedirectToAction("AddRaceTime", "Home");
            }

            var list = db.TimeRecords.Select(x => x.EventTitle).Distinct().ToList();
            ViewBag.EventTitle = new SelectList(list);


            ViewBag.AthleteID = new SelectList(db.Athletes, "ID", "Name", timeRecord.AthleteID);
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "MeetTitle", timeRecord.LocationID);
            return View(timeRecord);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
