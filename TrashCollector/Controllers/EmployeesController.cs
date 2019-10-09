using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentEmployee = db.Employees.Where(a => a.ApplicationId == currentUserId).Single();
            var customers = db.Customers.Where(c => c.ZipCode == currentEmployee.ZipCode).ToList();
            return View(customers);
        }


        //public List<Customer> GetCurrentDayCustomers()
        //{
        //    var employeeId = User.Identity.GetUserId();
        //    var currentEmployee = db.Employees.Where(e => e.ApplicationId == employeeId).Single();
        //    var dayOfWeekToday = DateTime.Now.DayOfWeek.ToString();
        //    var dateToday = DateTime.Now;
        //    var customerZipCodeMatch = db.Customers.Where(c => c.ZipCode == currentEmployee.ZipCode && currentEmployee.PickUpDay == dayOfWeekToday || c.ExtraPickupDay == dateToday).ToList();
        //    var customerSuspensionRemoved = customerZipCodeMatch.Where(c => (c.AccountSuspensionStartDate > dateToday && c.AccountSuspensionEndDate < dateToday)
        //    || (c.AccountSuspensionEndDate == null && c.AccountSuspensionStartDate == null)).ToList();
        //    return customerSuspensionRemoved;
        //}



        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ZipCode,")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeUserName,TodaysPickups,ZipCodes,HasPickUpBeenCompleted,HasChargeBeenApplied")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Filtering PickupDays(Incomplete)
        public ActionResult FilterPickupDays(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Customer customer = db.Customers.Find(id);

            var customer = db.Customers.Find(id);
            //customer.DaysOfWeek;
            db.SaveChanges();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }




        //GET: Confirm pickup!!
        public ActionResult ConfirmPickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Customer customer = db.Customers.Find(id);

            var customer = db.Customers.Find(id);
            customer.CustomerBalance -= 25;
            db.SaveChanges();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public ActionResult PickUps(int? id)
        {
            if (id == null)
            {
                Employee employee = db.Employees.Find(id);
                employee = db.Employees.Where(e => e.Id == employee.Id).Single();
                List<Customer> customersInArea = db.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList();//Need to add a day to the filter
                return View(customersInArea);
            }
            else
            {
                Employee employee = db.Employees.Find(id);
                employee = db.Employees.Where(e => e.Id == employee.Id).Single();
                List<Customer> customersInArea = db.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList();
                return View(customersInArea);
            }

        }


        public ActionResult Search(System.DayOfWeek? dayOfWeek)
        {
            var currentUId = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationId == currentUId).SingleOrDefault();
            var todaysPickups = db.Customers.Where(c => c.ZipCode == employee.ZipCode);
            var daysMatched = db.Customers.Where(c => c.DaysOfWeek.Equals(dayOfWeek));
            if (todaysPickups.Equals(null))
            {
                return View("Index");
            }
            else
            {
                return View(daysMatched);
            }
        }










    }
}
