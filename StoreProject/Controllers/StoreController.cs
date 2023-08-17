using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreProject.Controllers
{
    public class StoreController : Controller
    {
        AppDbContext _context;
        public StoreController()
        {
            _context = new AppDbContext();
        }
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllStores()
        {
            var stores = _context.stores.ToList();
            return PartialView(stores);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Store store)
        {
            _context.stores.Add(store);
            Space space = new Space { Store_Id = store.Store_Id };
            _context.spaces.Add(space);
            _context.SaveChanges();
            var stores = _context.stores.ToList();
            return PartialView("GetAllStores", stores);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var store = _context.stores.FirstOrDefault(i=>i.Store_Id == id);
            return PartialView(store);
        }
        [HttpPost]
        public ActionResult Edit(Store store)
        {
            _context.stores.AddOrUpdate(store);
            _context.SaveChanges();
            var stores = _context.stores.ToList();
            return PartialView("GetAllStores", stores);
        }
        [HttpGet]
        public ActionResult Details(int id) 
        {
            var store = _context.stores.FirstOrDefault(i => i.Store_Id == id);
            return PartialView(store);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var store = _context.stores.Include(s=>s.Spaces).FirstOrDefault(i => i.Store_Id == id);
            _context.spaces.RemoveRange(store.Spaces);
            _context.stores.Remove(store);
            _context.SaveChanges();
            var stores = _context.stores.ToList();
            return PartialView("GetAllStores", stores);
        }
        [HttpGet]
        public ActionResult GetAllSpaces(int id)
        {
            var store = _context.stores.Include(s=>s.Spaces).FirstOrDefault(s => s.Store_Id == id);
            ViewBag.Spaces = new SelectList(_context.spaces.ToList(), "Space_Id", "Space_Name", store.Store_Id);
            return View(store);
        }
        [HttpPost]
        public ActionResult Search()
        {
            bool ismain, isinvoice;
            string name = Convert.ToString(Request["name"]);
            string ismainch = Convert.ToString(Request["ismain"]);
            string isinvoicech = Convert.ToString(Request["isinvoice"]);
            if (ismainch == "on")
                ismain = true;
            else
                ismain = false;
            if (isinvoicech == "on")
                isinvoice = true;
            else
                isinvoice = false;
            var stores = _context.stores.Where(s => s.Store_Name.Contains(name) && s.IsMain == ismain && s.IsInvoiceDirect == isinvoice).ToList();
            return View(stores);
        }
    }
}