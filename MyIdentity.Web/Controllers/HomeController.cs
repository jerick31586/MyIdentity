using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers
{
    public class Departments
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //List<Departments> departments = new List<Departments>();
            //departments.Add(new Departments { DepartmentID = 1, DepartmentName = "IT" });
            //departments.Add(new Departments { DepartmentID = 2, DepartmentName = "HR" });
            //departments.Add(new Departments { DepartmentID = 3, DepartmentName = "SERVICE" });
            //departments.Add(new Departments { DepartmentID = 4, DepartmentName = "RECEPTION" });
            //departments.Add(new Departments { DepartmentID = 5, DepartmentName = "MARKETING" });

            //foreach (var item in departments)
            //{
            //    //new SelectListItem { }
            //}
            //ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
           
            return View();
        }
    }
}