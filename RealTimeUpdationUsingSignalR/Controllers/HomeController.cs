using RealTimeUpdationUsingSignalR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealTimeUpdationUsingSignalR.Controllers
{
    public class HomeController : Controller
    {
        List<Employee> empList;

        //Fetch Employee Records
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllEmployeeRecords()
        {
            using (var context = new EmployeeDBBEntities())
            {
                empList = context
                .Employees
                .ToList();
            }
            return PartialView("_EmployeeList", empList);
        }


        //Insert Employee Record
        public ActionResult Insert()
        {
            return View();
        }
        //ok
        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            if (ModelState.IsValid)
            {
                //Insert into Employee table 
                using (var context = new EmployeeDBBEntities())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }

            //Once the record is inserted , then notify all the subscribers (Clients)
            EmployeeHub.NotifyCurrentEmployeeInformationToAllClients();
            return RedirectToAction("Index");
        }

        //Update Employee Record
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            using (var context = new EmployeeDBBEntities())
            {
                var empRecord = context.Employees.Find(employee.EmployeeID);

                empRecord.EmployeeName = employee.EmployeeName;
                empRecord.EmailAdress = employee.EmailAdress;
                empRecord.MobileNumber = employee.MobileNumber;

                context.Employees.Add(empRecord);

                context.Entry(empRecord).State = EntityState.Modified;
                context.SaveChanges();
            }

            //Once the record is inserted , then notify all the subscribers (Clients)
            EmployeeHub.NotifyCurrentEmployeeInformationToAllClients();
            return RedirectToAction("Index");
        }

        //Delete Employee Record
        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            using (var context = new EmployeeDBBEntities())
            {
                var empRecord = context.Employees.Find(employee.EmployeeID);
                context.Employees.Remove(empRecord);
                context.SaveChanges();
            }

            //Once the record is inserted , then notify all the subscribers (Clients)
            EmployeeHub.NotifyCurrentEmployeeInformationToAllClients();
            return RedirectToAction("Index");
        }
    }
}