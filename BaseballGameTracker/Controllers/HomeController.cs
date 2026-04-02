using BaseballGameTracker.Application.Models;
using BaseballGameTracker.Application.Models.Games;
using BaseballGameTracker.Data;
using BaseballGameTracker.Models;
using BaseballGameTracker.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Claims;
using static System.Net.WebRequestMethods;


namespace BaseballGameTracker.Controllers
{
    public class HomeController(IRecordService _recordService, IGameService _gameService) : Controller
    {
        public async Task<IActionResult> Index()
        {

           

            var wins = _recordService.CalulateWins();
            var loses = _recordService.CalculateLoses();

            var getGameVM = await _gameService.GetTodaysGame(wins, loses);
  

            var record = new RecordVM
            {
                Wins = wins,
                Loses = loses
            };

            return View(getGameVM);
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
