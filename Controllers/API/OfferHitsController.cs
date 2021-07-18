using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Couponat.Models;

namespace Couponat.Controllers.API
{
    public class OfferHitsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        // POST: api/CouponHits
        [ResponseType(typeof(CouponHit))]
        [HttpPost]
        [Route("api/offers/AddOfferCouponHit")]
        public IHttpActionResult AddOfferCouponHit(OfferHit offerHit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            offerHit.Id = Guid.NewGuid();
            offerHit.CreationDate = DateTime.Now;
            offerHit.DeviceId = new Guid("6f3ff24a-ce53-4fc6-b3db-2a70d32c62fe");
            offerHit.HitType = HitTypes.Offer;
            db.OfferHits.Add(offerHit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CouponHitExists(offerHit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offerHit.Id }, offerHit);
        }

        [ResponseType(typeof(CouponHit))]
        [HttpPost]
        [Route("api/offers/addofferwebsitehit")]
        public IHttpActionResult AddOfferWebsiteHit(OfferHit offerHit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            offerHit.Id = Guid.NewGuid();
            offerHit.DeviceId = new Guid("6f3ff24a-ce53-4fc6-b3db-2a70d32c62fe");
            offerHit.HitType = HitTypes.OfferWebsite;
            offerHit.CreationDate = DateTime.Now;
            db.OfferHits.Add(offerHit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CouponHitExists(offerHit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offerHit.Id }, offerHit);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CouponHitExists(Guid id)
        {
            return db.CouponHits.Count(e => e.Id == id) > 0;
        }
    }
}