using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using lab5.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Objects;

namespace lab5.Controllers
{
    public class PersonController : Controller
    {
        private PhoneBookDbEntities _phoneBookDb;

        public PersonController()
        {
            _phoneBookDb = new PhoneBookDbEntities();
        }
        // GET: Person
        public ActionResult Index()
            
        {
            string user = User.Identity.GetUserId();
            var list1 = _phoneBookDb.People.Include(c => c.Contacts).Where(k => k.AddedBy == user).ToList();
            return View(list1);
            
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            var p = _phoneBookDb.People.Single(c => c.PersonId == id);
            return View(p);
        }

        // GET: Person/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(CollectionViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                string user = User.Identity.GetUserId();

                var person = new Person();
                person.FirstName = collection.Person.FirstName;
                person.MiddleName = collection.Person.MiddleName;
                person.LastName = collection.Person.LastName;
                person.DateOfBirth = collection.Person.DateOfBirth;
                person.FaceBookAccountId = collection.Person.FaceBookAccountId;
                person.LinkedInId = collection.Person.LinkedInId;
                person.EmailId = collection.Person.EmailId;
                person.AddedOn = DateTime.Now;
                person.HomeCity = collection.Person.HomeCity;
                person.UpdateOn = DateTime.Today;
                person.HomeAddress = collection.Person.HomeAddress;
                person.TwitterId = collection.Person.TwitterId;
                person.AddedBy = user;

                var contact = new Contact();
                contact.ContactNumber = collection.Contact.ContactNumber;
                contact.PersonId = collection.Person.PersonId;
                contact.Type = collection.Contact.Type;

                _phoneBookDb.People.Add(person);
                _phoneBookDb.Contacts.Add(contact);
                _phoneBookDb.SaveChanges();

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            var p = _phoneBookDb.People.Single(c => c.PersonId == id);
            var q = _phoneBookDb.Contacts.Single(c => c.PersonId == id);
            CollectionViewModel d = new CollectionViewModel
            {
                Person = p,
                Contact = q
            };
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(d);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CollectionViewModel collection)
        {
            //try
            //{
                // TODO: Add update logic here
            var person = _phoneBookDb.People.Single(c => c.PersonId == id);
            
            person.FirstName = collection.Person.FirstName;
            person.MiddleName = collection.Person.MiddleName;
            person.LastName = collection.Person.LastName;
            person.DateOfBirth = collection.Person.DateOfBirth;
            person.FaceBookAccountId = collection.Person.FaceBookAccountId;
            person.LinkedInId = collection.Person.LinkedInId;
            person.EmailId = collection.Person.EmailId;
            _phoneBookDb.People.Single(c => c.PersonId == id).UpdateOn = DateTime.Now.Date;


            person.HomeCity = collection.Person.HomeCity;
            person.HomeAddress = collection.Person.HomeAddress;
            person.TwitterId = collection.Person.TwitterId;
            

            var contact = new Contact();
            contact.ContactNumber = collection.Contact.ContactNumber;
            contact.Type = collection.Contact.Type;
            
            _phoneBookDb.SaveChanges();
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var p = _phoneBookDb.People.Single(c => c.PersonId == id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CollectionViewModel collection)
        {
            var p = _phoneBookDb.People.Single(e => e.PersonId == id);
            var c = _phoneBookDb.Contacts.Single(d => d.PersonId == id);
            _phoneBookDb.People.Remove(p);
            _phoneBookDb.Contacts.Remove(c);
            _phoneBookDb.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Birtday()
        {
            //var list = new List<Person>();
            //foreach(Person p in _phoneBookDb.People.ToList())
            //{
            //    DateTime birthday = Convert.ToDateTime(p.DateOfBirth);

            //    DateTime check = DateTime.Now;
            //    for(int i = 1; i < 10; i++)
            //    {
            //        if (birthday.Date == check.Date && birthday.Month == check.Month)
            //        {
            //            list.Add(p);
            //            check.AddDays(i);
            //        }
            //        else
            //        {
            //            check.AddDays(i);
            //        }
            //    }

            //}
            //var list = _phoneBookDb.People.Where(adr => adr.DateOfBirth != null).OrderBy(adr => EntityFunctions.DiffDays(DateTime.Today, EntityFunctions.AddYears(adr.DateOfBirth, EntityFunctions.DiffYears(adr.DateOfBirth, DateTime.Today) + ((Convert.ToDateTime(adr.DateOfBirth.Month) < DateTime.Today.Month || (adr.DateOfBirth.Day <= DateTime.Today.Day && adr.DateOfBirth.Month == DateTime.Today.Month)) ? 1 : 0)))).Take(10).ToList();
            
                                                                                                                                                       

            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
