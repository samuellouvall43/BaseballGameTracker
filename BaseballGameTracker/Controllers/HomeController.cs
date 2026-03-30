using BaseballGameTracker.Models;
using BaseballGameTracker.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace BaseballGameTracker.Controllers
{
    public class HomeController(IRecordService _recordService) : Controller
    {
        public IActionResult Index()
        {

            Console.WriteLine("======= HELLLOOOOOOO ==========="); 

            var wins = _recordService.CalulateWins();
            var loses = _recordService.CalculateLoses(); 

            var model = new RecordVM
            {
                Wins =  wins, 
                Loses = loses
            };
  

          
       
            return View(model);
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
    }
}
