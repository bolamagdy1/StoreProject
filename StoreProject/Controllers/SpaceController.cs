using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StoreProject.Controllers
{
    public class SpaceController : Controller
    {
        private readonly AppDbContext _context;
        public SpaceController()
        {
            _context = new AppDbContext();
        }
        // GET: Space
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllSpaces(int id)
        {
            var store = _context.stores.Include(s=>s.Spaces).FirstOrDefault(s => s.Store_Id == id);
            return PartialView(store);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var space = _context.spaces.FirstOrDefault(i=>i.Space_Id ==  id);
            return PartialView(space);
        }
        [HttpPost]
        public ActionResult Edit(Space space)
        {
            var oldspace = _context.spaces.FirstOrDefault(i=>i.Space_Id==space.Space_Id);
            oldspace.Space_Name = space.Space_Name;
            _context.SaveChanges();
            var store = _context.stores.Include(s=>s.Spaces).FirstOrDefault(i => i.Store_Id == oldspace.Store_Id);
            return PartialView("GetAllSpaces", store);
        }
        [HttpGet]
        public ActionResult Split(int id)
        {
            var space = _context.spaces.FirstOrDefault(i=>i.Space_Id==id);
            return PartialView(space);
        }
        [HttpPost]
        public ActionResult Split(Space input) 
        {
            int times = Convert.ToInt32(Request["No"]);
            var space = _context.spaces.FirstOrDefault(i => i.Space_Id == input.Space_Id);
            for (int i = 1; i < times; i++)
            {
                Space newspace = new Space{ Store_Id = space.Store_Id };
                _context.spaces.Add(newspace);
            }
            _context.SaveChanges();
            var store = _context.stores.Include(s=>s.Spaces).FirstOrDefault(i=>i.Store_Id == space.Store_Id);
            return PartialView("GetAllSpaces", store);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var space = _context.spaces.FirstOrDefault(i=>i.Space_Id == id);
            var oldstore = _context.stores.Include(s=>s.Spaces).FirstOrDefault(i => i.Store_Id == space.Store_Id);
            if (oldstore.Spaces.Count() > 1)
            {
                _context.spaces.Remove(space);
                _context.SaveChanges();
            }
            else
            {
                return JavaScript("window.alert('Can NOT Delete CUZ there is only one Space');");
            }
            var store = _context.stores.Include(s => s.Spaces).FirstOrDefault(i => i.Store_Id == space.Store_Id);
            return PartialView("GetAllSpaces", store);
        }
        [HttpGet]
        public ActionResult Merge(int id)
        {
            var space = _context.spaces.FirstOrDefault(i => i.Space_Id == id);
            var store = _context.stores.Include(s => s.Spaces).FirstOrDefault(i => i.Store_Id == space.Store_Id);
            ViewBag.Spaces = new SelectList(_context.spaces.Where(s=>s.Store_Id == store.Store_Id && s.Space_Id != space.Space_Id).ToList(),"Space_Id","Space_Name",space.Space_Id);
            
            if (Enumerable.Count(ViewBag.Spaces) > 0)
                return PartialView(space);
            else
                return JavaScript("window.alert('Can NOT Move CUZ there is only one Space');");
        }
        [HttpPost]
        public ActionResult Merge(Space space)
        {
            int choosen = Convert.ToInt32(Request["merged"]);
            var products = _context.products.Where(s => s.Space_Id == choosen).ToList();
            products.ForEach(p => p.Space_Id = space.Space_Id);

            var deleted = _context.spaces.FirstOrDefault(i => i.Space_Id == choosen);
            _context.spaces.Remove(deleted);
            _context.SaveChanges();

            var store = _context.stores.Include(s => s.Spaces).FirstOrDefault(i => i.Store_Id == deleted.Store_Id);
            return PartialView("GetAllSpaces", store);
        }
    }
}