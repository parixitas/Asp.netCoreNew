using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EmpContext _context;

        public HomeController(EmpContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        

        public IActionResult Index()
        {
            return View(_context.Employee.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Emp emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employee.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emp);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            Emp emp = _context.Employee.Find(id);
            if (emp == null)
            {
                return View("Index");
            }
            return View(emp);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            Emp emp = _context.Employee.Find(id);
            if (emp == null)
            {
                return View("Index");
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                Emp emp = _context.Employee.Find(id);
                _context.Employee.Remove(emp);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            Emp emp = _context.Employee.Find(id);
            if (emp == null)
            {
                return View("Index");
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Emp emp)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(emp).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
