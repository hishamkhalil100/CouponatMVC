using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Couponat.Models;

namespace Couponat.Controllers.API
{
    public class CouponHitsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


   
   
        // POST: api/CouponHits
        [ResponseType(typeof(CouponHit))]
        [Route("api/coupons/addcouponhit")]
        [HttpPost]
        
        public IHttpActionResult AddCouponHit(CouponHit couponHit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            couponHit.Id = Guid.NewGuid();
            couponHit.CreationDate = DateTime.Now;
            couponHit.DeviceId = new Guid("6f3ff24a-ce53-4fc6-b3db-2a70d32c62fe");
            couponHit.HitType = HitTypes.CouponCode;
            db.CouponHits.Add(couponHit);

            try
            {
                 db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CouponHitExists(couponHit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw; 
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = couponHit.Id }, couponHit);
        }

        [ResponseType(typeof(CouponHit))]
        [HttpPost]
        [Route("api/coupons/addcouponwebsitehit")]
        public IHttpActionResult AddCouponWebsiteHit(CouponHit couponHit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            couponHit.Id = Guid.NewGuid();
            couponHit.DeviceId = new Guid("6f3ff24a-ce53-4fc6-b3db-2a70d32c62fe");
            couponHit.HitType = HitTypes.CouponWebsite;
            couponHit.CreationDate = DateTime.Now;
            db.CouponHits.Add(couponHit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CouponHitExists(couponHit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = couponHit.Id }, couponHit);
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