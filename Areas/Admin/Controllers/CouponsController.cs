using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;
using Couponat.ViewModels;
using PagedList;

namespace Couponat.Areas.Admin.Controllers
{
    public class CouponsController : Controller
    {

        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        // GET: Coupons
       
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page )
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
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Coupons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = _unitOfWork.Coupons.Get(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // GET: Coupons/Create
        public ActionResult Create()
        {
            var stores = _unitOfWork.Stores.GetAll();
            var categories = _unitOfWork.Categories.GetAll();
            var formViewModel = new CouponFormViewModel()
            {
                Categories = categories,
                Stores = stores
            };
            return View(formViewModel);
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Coupon coupon)
        {
            if (ModelState.IsValid)
            {


                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = "";
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" ||
                            file.ContentType == "image/jpg" ||
                            file.ContentType == "image/gif" ||
                            file.ContentType == "image/png")
                        {
                            fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            fileName = Path.GetFileName(file.FileName);
                            var userfolderpath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            var fullPath = Server.MapPath("~/Images/") + file.FileName;
                            if (System.IO.File.Exists(fullPath))
                            {
                                ViewBag.ActionMessage = "Same File already Exists";
                            }
                            else
                            {
                                file.SaveAs(userfolderpath);
                                ViewBag.ActionMessage = "File has been uploaded successfully";
                            }
                        }
                        else
                        {
                            ViewBag.ActionMessage = "Please upload only imag (jpg,gif,png)";
                        }
                    }
                }



                coupon.Id = Guid.NewGuid();
                coupon.Logo = fileName;
                coupon.CreationDate = DateTime.Now;
                _unitOfWork.Coupons.Add(coupon);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Coupons/Edit/5
        public ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var coupon = _unitOfWork.Coupons.Get(id);


            var categories = _unitOfWork.Categories.GetAll();
            var stores = _unitOfWork.Stores.GetAll();
            var formViewModel = new CouponFormViewModel()
            {
                Categories =categories,
                Stores = stores,
                Coupon = coupon
            };
            

            if (formViewModel.Coupon== null)
            {
                return HttpNotFound();
            }
            return View(formViewModel);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Coupon coupon)
        {

            if (ModelState.IsValid)
            {

                var dbCoupon = _unitOfWork.Coupons.Get(coupon.Id);
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = dbCoupon.Logo;
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" ||
                            file.ContentType == "image/jpg" ||
                            file.ContentType == "image/gif" ||
                            file.ContentType == "image/png")
                        {
                            fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            fileName = Path.GetFileName(file.FileName);
                            var userfolderpath = Path.Combine(Server.MapPath("~/Images/"), fileName);

                            file.SaveAs(userfolderpath);
                            ViewBag.ActionMessage = "File has been uploaded successfully";
                            //Delete Old Image From Server 
                            
                            string fullPath = Request.MapPath("~/Images/" + dbCoupon.Logo);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                        }
                        else
                        {
                            ViewBag.ActionMessage = "Please upload only imag (jpg,gif,png)";
                        }
                    }
                }


                dbCoupon.CouponCode = coupon.CouponCode;
                dbCoupon.CouponDetails = coupon.CouponDetails;
                dbCoupon.CouponDetailsAr = coupon.CouponDetailsAr;
                dbCoupon.EndDate = coupon.EndDate;
                dbCoupon.Name = coupon.Name;
                dbCoupon.NameAr = coupon.NameAr;
                dbCoupon.Offer = coupon.Offer;
                dbCoupon.OfferAr = coupon.OfferAr;
                dbCoupon.Tags = coupon.Tags;
                dbCoupon.Website = coupon.Website;
                dbCoupon.CategoryId = coupon.CategoryId;
                dbCoupon.StoreId = coupon.StoreId;
                dbCoupon.Logo = fileName;
            
                _unitOfWork.Coupons.Edit(dbCoupon);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            
            }

            var formViewModel = new CouponFormViewModel()
            {
                Categories = _unitOfWork.Categories.GetAll(),
                Stores = _unitOfWork.Stores.GetAll(),
                Coupon = coupon
            };
            return View(formViewModel);
        }

        // GET: Coupons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = _unitOfWork.Coupons.Get(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Coupon coupon = _unitOfWork.Coupons.Get(id);
            if (coupon.CouponHits.Count > 0)
            {
                _unitOfWork.CouponHits.RemoveRange(coupon.CouponHits);
            }
            _unitOfWork.Coupons.Remove(coupon);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
