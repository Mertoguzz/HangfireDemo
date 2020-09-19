using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HangfireScheduleJobDemo_Authorization.Models;
using Hangfire;
using Microsoft.AspNetCore.Identity;

namespace HangfireScheduleJobDemo_Authorization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //    BackgroundJob.Enqueue(()=>Console.WriteLine(""));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> AddUserToAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("HFAdmin"));
            var user = await userManager.FindByNameAsync("test@gmail.com");
            await userManager.AddToRoleAsync(user, "HFAdmin");
            return Ok();
        }
    }
}
