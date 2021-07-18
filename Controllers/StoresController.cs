using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;
using PagedList;

namespace Couponat.Controllers
{
    public class StoresController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        // GET: Stores
        public ActionResult Index()
        {
            
            return View(_unitOfWork.Stores.GetAll());
        }

        public ActionResult Details(Guid id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var couponsInCategory = _unitOfWork.Stores.Get(id);
            if (couponsInCategory == null)
                return new HttpNotFoundResult();


          
                var store = _unitOfWork.Stores.GetAllCouponsInStore(id);

          
            return View(store);
        }
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var list = _unitOfWork.Stores.GetAll();


            return PartialView(list);
        }
    }
}