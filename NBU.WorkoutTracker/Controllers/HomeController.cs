using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NBU.WorkoutTracker.Models;
using Microsoft.Extensions.Logging;
namespace NBU.WorkoutTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly ILoggerFactory loggerFactory;

        public HomeController(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger("My logger");
        }
        
                    
        public IActionResult Index()
        {
            this.logger.LogInformation("Home page visit.");
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode != null && (statusCode == 404 || statusCode == 500))
            {
                string viewName = statusCode.ToString();

                return View(viewName);
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
