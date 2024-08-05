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
    public class GeneralListCandidatesController : Controller
    {
        private ElectionEntities1 db = new ElectionEntities1();

        
        // GET: GeneralListCandidates
        public ActionResult Index()
        {
            var generalListCandidates = db.GeneralListCandidates.Include(g => g.GeneralListing);
            return View(generalListCandidates.ToList());
        }

        [HttpPost]
        public ActionResult UpdateStatus(int CandidateID, string Status)
        {
            var candidate = db.GeneralListCandidates.Find(CandidateID);
            if (candidate != null)
            {
                candidate.Status = Status;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        // GET: GeneralListCandidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralListCandidate generalListCandidate = db.GeneralListCandidates.Find(id);
            if (generalListCandidate == null)
            {
                return HttpNotFound();
            }
            return View(generalListCandidate);
        }

        /* // GET: GeneralListCandidates/Create
         public ActionResult Create()
         {
             ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name");
             return View();
         }

         // POST: GeneralListCandidates/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "CandidateID,GeneralListingName,CandidateName,Email,Status")] GeneralListCandidate generalListCandidate)
         {
             if (ModelState.IsValid)
             {
                 db.GeneralListCandidates.Add(generalListCandidate);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name", generalListCandidate.GeneralListingName);
             return View(generalListCandidate);
         }*/




        // GET: GeneralListCandidates/Create
        public ActionResult Create()
        {
            ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name");
            return View();
        }

        // POST: GeneralListCandidates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateID,GeneralListingName,CandidateName,Email,Status")] GeneralListCandidate generalListCandidate)
        {
            if (ModelState.IsValid)
            {
                if (generalListCandidate.Status == "1")
                {
                    int count = db.GeneralListCandidates.Count(c => c.GeneralListingName == generalListCandidate.GeneralListingName && c.Status == "1");
                    if (count >= 3)
                    {
                        ModelState.AddModelError("GeneralListingName", "The number of accepted candidates for the same General Listing Name cannot exceed 41.");
                        ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name", generalListCandidate.GeneralListingName);
                        return View(generalListCandidate);
                    }
                }

                db.GeneralListCandidates.Add(generalListCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name", generalListCandidate.GeneralListingName);
            return View(generalListCandidate);
        }










        // GET: GeneralListCandidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralListCandidate generalListCandidate = db.GeneralListCandidates.Find(id);
            if (generalListCandidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name", generalListCandidate.GeneralListingName);
            return View(generalListCandidate);
        }

        // POST: GeneralListCandidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateID,GeneralListingName,CandidateName,Email,Status")] GeneralListCandidate generalListCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generalListCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneralListingName = new SelectList(db.GeneralListings, "Name", "Name", generalListCandidate.GeneralListingName);
            return View(generalListCandidate);
        }

        // GET: GeneralListCandidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralListCandidate generalListCandidate = db.GeneralListCandidates.Find(id);
            if (generalListCandidate == null)
            {
                return HttpNotFound();
            }
            return View(generalListCandidate);
        }

        // POST: GeneralListCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneralListCandidate generalListCandidate = db.GeneralListCandidates.Find(id);
            db.GeneralListCandidates.Remove(generalListCandidate);
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
