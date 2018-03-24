// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MvcSandbox.ActionConstraints;
using MvcSandbox.ActionFilters;
using MvcSandbox.AuthorizationFilters;
using MvcSandbox.ResultFilters;

namespace MvcSandbox.Controllers
{
    public class HomeController : Controller
    {
        [ModelBinder]
        public string Id { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [IsMobile]
        [ActionName("Index")]
        public IActionResult IndexMobile()
        {
            return Content("The Mobile Index View");
        }

        public IActionResult GetCustomer()
        {
            return Ok(new { Id = 3, FirstName = "Test", LastName = "Sample" });
        }

        [LocalAuthorize]
        public IActionResult Edit()
        {
            return Content("The Edit Action Method");
        }

        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            return Content("The Edit Action Method with parameters");
        }

        [LogAction]
        [ResultLog]
        public IActionResult Splash()
        {
            return Content("This is the splash page!");
        }
    }
}
