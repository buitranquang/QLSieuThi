using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLySieuThi.DTO;
using QuanLySieuThi.BUS;
using System.Web.Mvc;

namespace QuanLySieuThi.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            EmployeeBUS employeeBUS = new EmployeeBUS();
            ViewBag.Employees = employeeBUS.GetAllEmployees();
            return View();
        }

        public ActionResult Edit(int id)
        {
            EmployeeBUS employeeBUS = new EmployeeBUS();
            Employee emp = employeeBUS.GetEmployeeById(id);
            ViewBag.Employee = emp;

            return View(emp);
        }

        [HttpPost]
        public ActionResult EditEmployee()
        {
            string ID = Request.Form["ID"];
            string name = Request.Form["name"];
            string phone = Request.Form["phone"];
            string adress = Request.Form["address"];
            string Role = Request.Form["Role"];
            string username = Request.Form["username"];
            EmployeeBUS employeeBUS = new EmployeeBUS();
            employeeBUS.UpdateEmployeeInfo(ID, name, phone, adress, Role);

            return RedirectToAction("Index", "User");
        }
        public ActionResult Delete(int id)
        {
            EmployeeBUS employeeBUS = new EmployeeBUS();
            employeeBUS.DeleteEmployee(id);
            return RedirectToAction("Index", "User");
        }

        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee()
        {
            string username = Request.Form["username"];
            string fullname = Request.Form["fullname"];
            string phone = Request.Form["phone"];
            string adress = Request.Form["address"];
            string password = Request.Form["password"];
            string role = Request.Form["role"];
            Employee emp = new Employee(fullname, adress, phone, username, password, role);
            EmployeeBUS employeeBUS = new EmployeeBUS();
            employeeBUS.AddEmployee(emp);
            return RedirectToAction("Index", "User");
        }
    }
}