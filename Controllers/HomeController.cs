using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GuessGame.Models;
using Microsoft.AspNetCore.Http;
using GuessGame.Database;

namespace GuessGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var db = new UsersDB();

            return View(db.indexInfo());
        }


        public IActionResult Admin()
        {
            var viewModel = new AdminViewModel();
            
            viewModel.Users = new UsersDB().GetAll();
            viewModel.Games = new GameDB().GetAll();
            return View(viewModel);
        }

        
        



        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult TermsOfService()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult TheGame()
        {
            return View(new GameDB().GetGameId());
        }
        
       
        
        public IActionResult MyPage()
        {
            var db = new GameDB();
            return View(db.indexInfo());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
