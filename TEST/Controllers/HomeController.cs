﻿using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEST.Controllers
{
    public class HomeController : Controller
    {
        private IUserBAL _userBAL = null;

        public HomeController(IUserBAL userBAL)
        {
            _userBAL = userBAL;
        }

        [Authorize]
        public ActionResult Index()
        {
            _userBAL.Get();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}