using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;
using PagedList;

namespace Couponat.Areas.Admin.Controllers
{
    public class BannersController : Controller
    {
       
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        // GET: Admin/Banners
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
            var list = _unitOfWork.Banners.GetAll();
         
            
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<Banner>(list.OrderByDescending(s => s.Url));
                    break;
                case "Date":
                    list = new List<Banner>(list.OrderBy(s => s.EndDate));
                    break;
                case "date_desc":
                    list = new List<Banner>(list.OrderByDescending(s => s.EndDate));
                    break;
                default:  // Name ascending 
                    list = new List<Banner>(list.OrderByDescending(s => s.EndDate)); break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Banners/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = _unitOfWork.Banners.Get(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Admin/Banners/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Banner banner)
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



                banner.Id = Guid.NewGuid();
                banner.Image = fileName;
                _unitOfWork.Banners.Add(banner);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Admin/Banners/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = _unitOfWork.Banners.Get(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Banner banner)
        {
            if (ModelState.IsValid)
            {

                var dbStore = _unitOfWork.Banners.Get(banner.Id);
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = dbStore.Image;
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

                            string fullPath = Request.MapPath("~/Images/" + dbStore.Image);
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



                dbStore.Image = fileName;
                dbStore.Url = banner.Url;
                dbStore.EndDate = banner.EndDate;
                dbStore.IsVisible = banner.IsVisible;
                _unitOfWork.Banners.Edit(dbStore);
                _unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = _unitOfWork.Banners.Get(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Banner banner = _unitOfWork.Banners.Get(id);
            _unitOfWork.Banners.Remove(banner);
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
