using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.Models;
using HW6.Models.ViewModels;

namespace HW6.Controllers
{
    public class HomeController : Controller
    {
        private WWImContext db = new WWImContext();

        // GET: StockItems
        public ActionResult Index(string search)
        {

            IEnumerable<HW6.Models.StockItem> searchItems = db.StockItems.Where(p => p.StockItemName.Contains(search)).ToList();

            return View(searchItems);

        }

        // GET: StockItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockItem stockItem = db.StockItems.Find(id);

            if (stockItem == null)
            {
                return HttpNotFound();
            }
            ItemDetailsViewModel viewModel = new ItemDetailsViewModel(stockItem);
            return View(viewModel);
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
