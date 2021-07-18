using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;
using PagedList;

namespace Couponat.Controllers
{
    public class CategoriesController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext()); 
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var couponsInCategory = _unitOfWork.Categories.Get((int) id);
            if (couponsInCategory == null)
                return new HttpNotFoundResult();

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
                list = _unitOfWork.Categories.GetAllCouponsInCategory((int)id).Coupons.ToList();
            }
            else
            {
                list = _unitOfWork.Categories.GetAllCouponsInCategory((int)id).Coupons.Where(c => c.Name.Contains(searchString)|| c.NameAr.Contains(searchString)).ToList();
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
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var list = _unitOfWork.Categories.GetAll();
            

            return PartialView(list);
        }
    }
}