using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Voting.Models;

namespace E_Voting.Controllers
{
    public class AdminController : Controller
    {
        private ElectionEntities1 db = new ElectionEntities1();

        // GET: Admin

        // GET: contact_us
        public ActionResult Index(bool? isRead)
        {
            var contacts = db.Contacts.AsQueryable();

            if (isRead.HasValue)
            {
                if (isRead.Value)
                {
                    contacts = contacts.Where(c => c.IsRead);
                }
                else
                {
                    contacts = contacts.Where(c => !c.IsRead);
                }
            }

            return View(contacts.ToList());
        }







        // GET: contact_us/Details/5
        public ActionResult DetailsContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            // Set IsRead to true
            contact.IsRead = true;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            return View(contact);

        }












        /*
                // GET: User/DetailsContact/5
                public ActionResult DetailsContact(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Contact contact = db.Contacts.Find(id);
                    if (contact == null)
                    {
                        return HttpNotFound();
                    }
                    return View(contact);
                }*/



        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,SenderName,SenderEmail,Message,SubmissionDate")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
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
    }
}
