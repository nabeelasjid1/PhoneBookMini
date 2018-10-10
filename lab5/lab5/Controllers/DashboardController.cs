using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab5.Controllers
{
    public class DashboardController : Controller
    {
        private PhoneBookDbEntities _phoneBookDb;

        public DashboardController()
        {
            _phoneBookDb = new PhoneBookDbEntities();
        }
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var people = from r in _phoneBookDb.People where r.AspNetUser.Id == user select r;
            List<Person> birthdays = new List<Person>();
            List<Person> updated = new List<Person>();
            foreach (Person person in people.ToList())
            {
                if ((person.DateOfBirth.Value.DayOfYear - DateTime.Today.DayOfYear <= 10) && (person.DateOfBirth.Value.DayOfYear - DateTime.Today.DayOfYear >= 0))
                {
                    birthdays.Add(person);
                }
            }
            foreach (Person person in people.ToList())
            {
                    if ((DateTime.Today.DayOfYear - person.AddedOn.DayOfYear) <= 7)
                    {
                        updated.Add(person);
                    }
                
            }
            ViewBag.PersonCount = people.ToList().Count.ToString();
            ViewBag.Birthdays = birthdays;
            @ViewBag.Updated = updated;
            return View();
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}