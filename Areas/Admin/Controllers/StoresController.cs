using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Couponat.Helper;
using Couponat.Models;
using Couponat.Persistence;

namespace Couponat.Areas.Admin.Controllers
{
    public class StoresController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        // GET: Admin/Stores
        public ActionResult Index()
        {
            return View(_unitOfWork.Stores.GetAll());
        }

        // GET: Admin/Stores/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _unitOfWork.Stores.Get(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Admin/Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Store store )
        {
            if (ModelState.IsValid)
            {
                store.Id = Guid.NewGuid();
                store.Logo = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
                _unitOfWork.Stores.Add(store);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Admin/Stores/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _unitOfWork.Stores.Get(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Admin/Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Store store)
        {
            if (ModelState.IsValid)
            {

                var dbStore = _unitOfWork.Stores.Get(store.Id);

                CustomHelper.DeleteImg(dbStore.Logo,Server);
                dbStore.Name = store.Name;
                dbStore.NameAr = store.NameAr;
                dbStore.Tags = store.Tags;
                dbStore.Logo = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
                _unitOfWork.Stores.Edit(dbStore);
                _unitOfWork.Complete();
                
                return RedirectToAction("Index");

            }
            return View(store);
        }

        // GET: Admin/Stores/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = _unitOfWork.Stores.Get(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Admin/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Store store = _unitOfWork.Stores.Get(id);
            CustomHelper.DeleteImg(store.Logo, Server);
            _unitOfWork.Stores.Remove(store);
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
