using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBapp.Controllers
{
    public class ShiftsController : Controller
    {
        // GET: Shifts
        public ActionResult Index()
        {
            return View("ShiftForm");
        }
    }
}