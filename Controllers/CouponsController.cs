using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;
using PagedList;

namespace Couponat.Controllers
{
    public class CouponsController : Controller
    {
        // GET: Coupons
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        // GET: Coupons

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var list = new List<Coupon>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = _unitOfWork.Coupons.GetCouponsWithHits();
            }
            else
            {
                list = _unitOfWork.Coupons.GetCouponsWithHits(searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<Coupon>(list.OrderByDescending(s => s.Name));
                    break;
                case "Date":
                    list = new List<Coupon>(list.OrderBy(s => s.CreationDate));
                    break;
                case "date_desc":
                    list = new List<Coupon>(list.OrderByDescending(s => s.CreationDate));
                    break;
                default:  // Name ascending 
                    list = new List<Coupon>(list.OrderByDescending(s => s.CouponHits.Count(c => c.HitType == HitTypes.CouponCode)));
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.MataTag = "test, test, test";
            return View(list.ToPagedList(pageNumber, pageSize));
        }

       

    }
}