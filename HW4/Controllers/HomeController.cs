using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Mvc;

namespace HW4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RGBColor()
        {
            if (ModelState.IsValid)
            {
                ViewBag.red = (Request.QueryString["Red"]);
                ViewBag.green = (Request.QueryString["Green"]);
                ViewBag.blue = (Request.QueryString["Blue"]);


                Color myColor = Color.FromArgb(Convert.ToInt32(ViewBag.red), Convert.ToInt32(ViewBag.green), Convert.ToInt32(ViewBag.blue));


                ViewBag.htmlColor = ColorTranslator.ToHtml(myColor);

                return View();

            }

            

            return View("RGBColor");





        }
        
        

        public ActionResult ColorIP()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}