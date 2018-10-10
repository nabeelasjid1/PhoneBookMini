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
            var users = _phoneBookDb.People.Where(p => p.AddedBy == user).ToList();
            int no_of_persons = users.Count();
            int users_with_birthday = 0;
            int updated_users = 0;
            /////Persons Whose Data is updated in Last 7 days

            List<DateTime> Updated_users_list = new List<DateTime>();
            DateTime datetime_now = DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                DateTime updated_time = datetime_now.AddDays(-i);
                Updated_users_list.Add(updated_time);
            }
            foreach (var up in users)
            {
                var UpDated = Convert.ToDateTime(up.UpdateOn);
                for (int i = 0; i < 7; i++)
                {
                    if (UpDated.Day == Updated_users_list[i].Day && UpDated.Month == Updated_users_list[i].Month)
                    {
                        updated_users++;
                    }
                }
            }
            List<DateTime> Birthday_users_list = new List<DateTime>();
            DateTime date_now = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                DateTime bdate_next = date_now.AddDays(i);
                Birthday_users_list.Add(bdate_next);
            }
            foreach (var bd in users)
            {
                var Bdate = Convert.ToDateTime(bd.DateOfBirth);
                for (int i = 0; i < 7; i++)
                {
                    if (Bdate.Day == Birthday_users_list[i].Day && Bdate.Month == Birthday_users_list[i].Month)
                    {
                        users_with_birthday++;
                    }
                }
            }
            ViewBag.Total_no_Persons = no_of_persons;
            ViewBag.Persons_upcomming_BD = users_with_birthday;
            ViewBag.Updated_Persons_Data = updated_users;


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