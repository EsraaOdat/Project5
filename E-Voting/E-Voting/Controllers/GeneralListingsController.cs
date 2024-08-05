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
    public class GeneralListingsController : Controller
    {
        private ElectionEntities1 db = new ElectionEntities1();

        // GET: GeneralListings
        public ActionResult Index()
        {
            return View(db.GeneralListings.ToList());
        }

        // GET: GeneralListings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralListing generalListing = db.GeneralListings.Find(id);
            if (generalListing == null)
            {
                return HttpNotFound();
            }
            return View(generalListing);
        }

        // GET: GeneralListings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeneralListingID,Name,NumberOfVotes")] GeneralListing generalListing)
        {
            if (ModelState.IsValid)
            {
                db.GeneralListings.Add(generalListing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generalListing);
        }





        public ActionResult Edit(int id)
        {
            var candidate = db.GeneralListCandidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }

            // Create a SelectList for Status
            ViewBag.StatusList = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Text = "Reject", Value = "0" },
        new SelectListItem { Text = "Accept", Value = "1" }
    }, "Value", "Text", candidate.Status); // Set the selected value

            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GeneralListCandidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Re-populate the SelectList in case of error
            ViewBag.StatusList = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Text = "Reject", Value = "0" },
        new SelectListItem { Text = "Accept", Value = "1" }
    }, "Value", "Text", candidate.Status);

            return View(candidate);
        }










        // GET: GeneralListings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralListing generalListing = db.GeneralListings.Find(id);
            if (generalListing == null)
            {
                return HttpNotFound();
            }
            return View(generalListing);
        }

        // POST: GeneralListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GeneralListing generalListing = db.GeneralListings.Find(id);
            db.GeneralListings.Remove(generalListing);
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
