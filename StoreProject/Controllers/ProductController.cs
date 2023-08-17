using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController()
        {
            _context = new AppDbContext();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add_Product(int id)
        {
            ViewBag.SpaceId = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add_Product(Product product)
        {
            int spaceid = Convert.ToInt32(Request["temp"]);
            product.Space_Id = spaceid;
            _context.products.Add(product);
            _context.SaveChanges();
            var space = _context.spaces.Include(p=>p.Products).FirstOrDefault(i=>i.Space_Id == product.Space_Id);
            return PartialView("GetAllProducts",space);
        }
        public ActionResult GetAllProducts(int id)
        {
            var space = _context.spaces.Include(p=>p.Products).FirstOrDefault(s=>s.Space_Id == id);
            return PartialView(space);
        }
        [HttpGet]
        public ActionResult Move(int id)
        {
            var product = _context.products.FirstOrDefault(i => i.Product_Id == id);
            var space = _context.spaces.Include(p => p.Products).FirstOrDefault(s => s.Space_Id == product.Space_Id);
            ViewBag.Spaces = new SelectList(_context.spaces.Where(s=>s.Store_Id == space.Store_Id && s.Space_Id != space.Space_Id).ToList(),"Space_Id","Space_Name",space.Space_Id);

            if (Enumerable.Count(ViewBag.Spaces) > 0)
                return PartialView(product);
            else
                return JavaScript("window.alert('Can NOT Move CUZ there is only one Space');");
        }
        [HttpPost]
        public ActionResult Move(Space newspace) 
        {
            int temp = Convert.ToInt32(Request["moved"]);
            var product = _context.products.FirstOrDefault(i => i.Product_Id == temp);
            product.Space_Id = newspace.Space_Id;
            _context.SaveChanges();
            var space = _context.spaces.Include(p=>p.Products).FirstOrDefault(i=>i.Space_Id == newspace.Space_Id);
            return PartialView("GetAllProducts", space);
        }
    }
}