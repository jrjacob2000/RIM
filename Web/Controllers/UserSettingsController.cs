using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class UserSettingsController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserSettings
        public ActionResult Index()
        {
            return View(db.UserSettings.ToList());
        }

        // GET: UserSettings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSetting userSetting = db.UserSettings.Find(UserId);
            if (userSetting == null)
            {
                return HttpNotFound();
            }
            return View(userSetting);
        }

        // GET: UserSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,BusinessName,InvoiceNumber,PurchaseNumber,SalesNumber")] UserSetting userSetting)
        {
            try
            {
                userSetting.UserId = UserId;//Guid.NewGuid();
                //if (ModelState.IsValid)
                //{                
                db.UserSettings.Add(userSetting);
                db.SaveChanges();
                //return RedirectToAction("Index");
                //}

                return View(userSetting);
            }
            catch(Exception ex)        
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View();
            }
        }

        // GET: UserSettings/Edit/5
        public ActionResult Edit()
        {
            //if (id == null)
            //{
               
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            UserSetting userSetting = db.UserSettings.Find(UserId);
            if (userSetting == null)
            {
                return View(new UserSetting());
                //return HttpNotFound();
            }
            return View(userSetting);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( UserSetting userSetting)
        {
            var userId = new Guid( User.Identity.GetUserId());

            var dbUserSetting = db.UserSettings.Find(userId);
            if (dbUserSetting == null)
            {
                return Create(userSetting);
            }
            else
            {
                dbUserSetting.BusinessName = userSetting.BusinessName;
                dbUserSetting.BillingAddress = userSetting.BillingAddress;
                dbUserSetting.ShippingAddress = userSetting.ShippingAddress;
                dbUserSetting.BusinessContactNumber = userSetting.BusinessContactNumber;
                dbUserSetting.InvoicePrefix = userSetting.InvoicePrefix;
                dbUserSetting.InvoiceNumber = userSetting.InvoiceNumber;
                dbUserSetting.SalesPrefix = userSetting.SalesPrefix;
                dbUserSetting.SalesNumber = userSetting.SalesNumber;
                dbUserSetting.PurchasePrefix = userSetting.PurchasePrefix;
                dbUserSetting.PurchaseNumber = userSetting.PurchaseNumber;
                if (ModelState.IsValid)
                {
                    db.Entry(dbUserSetting).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    ViewBag.Message = "Saving Successful.";
                }
                return View(dbUserSetting);
            }
        }

        // GET: UserSettings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSetting userSetting = db.UserSettings.Find(id);
            if (userSetting == null)
            {
                return HttpNotFound();
            }
            return View(userSetting);
        }

        // POST: UserSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserSetting userSetting = db.UserSettings.Find(id);
            db.UserSettings.Remove(userSetting);
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
