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
    public class OffersController : Controller
    {
        // GET: Offers
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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
            var list = new List<Offer>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = _unitOfWork.Offers.GetOffersWithHits();
            }
            else
            {
                list = _unitOfWork.Offers.GetOffersWithHits(searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<Offer>(list.OrderByDescending(s => s.Name));
                    break;
                case "Date":
                    list = new List<Offer>(list.OrderBy(s => s.CreationDate));
                    break;
                case "date_desc":
                    list = new List<Offer>(list.OrderByDescending(s => s.CreationDate));
                    break;
                default:  // Name ascending 
                    list = new List<Offer>(list.OrderByDescending(s => s.OfferHits.Count(c => c.HitType == HitTypes.Offer)));
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Offers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer Offer = _unitOfWork.Offers.Get(id);
            if (Offer == null)
            {
                return HttpNotFound();
            }
            return View(Offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            var stores = _unitOfWork.Stores.GetAll();
            var categories = _unitOfWork.Categories.GetAll();
            var formViewModel = new OfferFormViewModel()
            {
                Categories = categories,
                Stores = stores
            };
            return View(formViewModel);
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Offer Offer)
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



                Offer.Id = Guid.NewGuid();
                Offer.Image = fileName;
                Offer.CreationDate =DateTime.Now;
                _unitOfWork.Offers.Add(Offer);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Offer = _unitOfWork.Offers.Get(id);
            
            var categories = _unitOfWork.Categories.GetAll();
            var stores = _unitOfWork.Stores.GetAll();
            var formViewModel = new OfferFormViewModel()
            {
                Categories = categories,
                Stores = stores,
                Offer = Offer
            };


            if (formViewModel.Offer == null)
            {
                return HttpNotFound();
            }
            return View(formViewModel);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offer Offer)
        {

            if (ModelState.IsValid)
            {

                var dbOffer = _unitOfWork.Offers.Get(Offer.Id);
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = dbOffer.Image;
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
                            
                            var userfolderpath = Path.Combine(Server.MapPath("~/Images/"), fileName);

                            file.SaveAs(userfolderpath);
                            ViewBag.ActionMessage = "File has been uploaded successfully";
                            //Delete Old Image From Server 

                            string fullPath = Request.MapPath("~/Images/" + dbOffer.Image);
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


                dbOffer.IsSpecialOffer = Offer.IsSpecialOffer;
                dbOffer.OfferDetails = Offer.OfferDetails;
                dbOffer.OfferDetailsAr = Offer.OfferDetailsAr;
                dbOffer.EndDate = Offer.EndDate;
                dbOffer.Name = Offer.Name;
                dbOffer.NameAr = Offer.NameAr;
              
                dbOffer.Tags = Offer.Tags;
                dbOffer.Website = Offer.Website;
               // dbOffer.CategoryId = Offer.CategoryId;
              //  dbOffer.StoreId = Offer.StoreId;
                dbOffer.Image = fileName;

                _unitOfWork.Offers.Edit(dbOffer);
                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }

            var formViewModel = new OfferFormViewModel()
            {
                Categories = _unitOfWork.Categories.GetAll(),
                Stores = _unitOfWork.Stores.GetAll(),
                Offer = Offer
            };
            return View(formViewModel);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer Offer = _unitOfWork.Offers.Get(id);
            if (Offer == null)
            {
                return HttpNotFound();
            }
            return View(Offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Offer Offer = _unitOfWork.Offers.Get(id);
            if (Offer.OfferHits.Count>0)
            {
                _unitOfWork.OfferHits.RemoveRange(Offer.OfferHits);
            }
            
            _unitOfWork.Offers.Remove(Offer);
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
