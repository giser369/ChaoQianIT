﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCExercise.Controllers
{
    public class toolController : Controller
    {
        //
        // GET: /tool/

        public ActionResult Index(string name)
        {
            return View();
        }

    }
}
