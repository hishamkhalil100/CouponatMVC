using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couponat.Models;
using Couponat.Persistence;

namespace Couponat.Controllers
{
    public class BannersController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        // GET: Banners
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var list = _unitOfWork.Banners.GetAll();


            return PartialView(list);
        }
    }
}