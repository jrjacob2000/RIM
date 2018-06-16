using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class PartnersController : ControllerBase
    {
        //private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: Partners
        public ActionResult Index()
        {
            var partners = GetPartnerList()
                .ToList()
                .Select(x => new Partner() { 
                    Id = x.Id,
                    Name = x.Name,
                    BillingAddress = x.BillingAddress,
                    ShippingAddress = x.ShippingAddress,
                    Email = x.Email,
                    Contact =x.Contact,
                    UnpaidInvoices = GetInvoiceByPartnerId(x.Id).Where(s => s.Status != Helper.Constants.InvoiceStatus.PAID).ToList(),
                    UnpaidCreditNotes = GetCreditByPartnerId(x.Id).Where(s => s.Status != Helper.Constants.CreditNotesStatus.PAID).ToList()
                })
                .ToList();
            return View(partners);
        }

        // GET: Partners/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = GetPartnerById(id); //db.Partners.Find(id);
            partner.Payments = GetPaymentList().Where(x => x.Partner_Id == id).ToList();
            partner.UnpaidInvoices = GetInvoiceByPartnerId(id)
                                .Where(x => x.Status != Helper.Constants.InvoiceStatus.PAID)
                                .ToList();
            partner.UnpaidCreditNotes = GetCreditByPartnerId(id)
                                .Where(x => x.Status != Helper.Constants.CreditNotesStatus.PAID)
                                .ToList();
            
            

            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // GET: Partners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ShippingAddress,BillingAddress,Email,Phone,Contact")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                partner.Id = Guid.NewGuid();
                partner.CreatedBy = UserId;
                db.Partners.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partner);
        }

        // GET: Partners/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = GetPartnerById(id.Value); //db.Partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ShippingAddress,BillingAddress,Email,Phone,Contact")] Partner partner)
        {
            var dbPartner = GetPartnerById(partner.Id, true);
            partner.CreatedBy = dbPartner.CreatedBy;
            
            if (ModelState.IsValid)
            {
                db.Entry(partner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partner);
        }

        // GET: Partners/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = GetPartnerById(id.Value); //db.Partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Partner partner = GetPartnerById(id); //db.Partners.Find(id);
            try
            {                
                db.Partners.Remove(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var sqlException = ex.InnerException.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 547/*DELETE statement conflicted */))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete this record, its already being used by other records");
                    return View(partner);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(partner);
                }
            }
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
