﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartOffice.Controllers
{
    public class CCPSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}