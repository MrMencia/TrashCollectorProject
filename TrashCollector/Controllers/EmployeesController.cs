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
            var currentEmployee = db.Employees.Where(a => a.ApplicationId == currentUserId);
            return View(currentEmployee);
        }

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

        ////Pickups by Day!!
        //public ActionResult PickupsByDay(int DayOfWeek)
        //{
        //    string currentUserId = User.Identity.GetUserId();
        //    Employee employee = db.Employees.Where(e => e.ApplicationId == currentUserId).Single();
        //    DateTime todaysDate = new DateTime();
        //    var customerPickup = db.Customers.Where(c => c.ZipCode == employee.ZipCode && ((int)c. RegularPickupDay == DayOfWeek)).Include(c => c.Pickup).ToList();

        //    for (int i = 0; i < customerPickup.count; i++)
        //    {
        //        if (customerPickup[i].Pickup.TemporarySuspensionStart != null && customerPickup[i].Pickup.TemporarySuspensionEnd != null)
        //        {
        //            if (todaysDate.Ticks > ((DateTime)customerPickup[i].Pickup.TemporarySuspensionStart).Ticks && todaysDate.Ticks < ((DateTime)customerPickup[i].Pickup.TemporarySuspensionEnd).Ticks)
        //            {
        //                customerPickup.RemoveAt(i);
        //            }
        //        }
        //    }
        //    return View(customerPickup);

        //}

        //GET: Confirm pickup!!
        public ActionResult ConfirmPickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Customer customer = db.Customers.Find(id);
            Customer customer = db.Customers.Where(c => c.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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
