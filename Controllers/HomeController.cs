using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojo_dachi.Controllers
{
    public class HomeController : Controller
    {
        public bool Chance()
        {
            Random rand = new Random();
            int chance = rand.Next(4);
            if(chance == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // public int 
        // GET: /Home/
        [HttpGet]
        [Route("dojodachi")]
        public IActionResult Index()
        {
            
            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            if(HttpContext.Session.GetInt32("Fullness") == null)
            {
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
            }  
            
            if(HttpContext.Session.GetInt32("Fullness") > 100 && HttpContext.Session.GetInt32("Energy") > 100 && HttpContext.Session.GetInt32("Happiness") > 50)
            {
                TempData["message"] = "You win!";
            }

            if(HttpContext.Session.GetInt32("Fullness") <= 0 || HttpContext.Session.GetInt32("Happiness") <= 0)
            {
                TempData["message"] = "You Lose";
            }
            // HttpContext.Session.SetInt32("Happiness", 20);
            // HttpContext.Session.SetInt32("Fullness", 20);
            // HttpContext.Session.SetInt32("Energy", 50);
            // HttpContext.Session.SetInt32("Meals", 3);

            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.message = TempData["message"];
            return View("Index");
        }

        [HttpGet("/feed")]
        public IActionResult Feed()
        {
            Random rand = new Random();
            int increase = rand.Next(5,10);
            
            // ViewBag.Fullness+=increase;
            if(Chance())
            {
            int? Meals = HttpContext.Session.GetInt32("Meals") - 1;
            HttpContext.Session.SetInt32("Meals", (int) Meals);
            // Fullness+=increase;
            }
            else{
                int? Fullness = HttpContext.Session.GetInt32("Fullness") + increase;
                HttpContext.Session.SetInt32("Fullness", (int) Fullness);
                int? Meals = HttpContext.Session.GetInt32("Meals") - 1;
                HttpContext.Session.SetInt32("Meals", (int) Meals);
            }
            TempData["message"] = $"You fed your dojodachi, Meals -1, Fullness {increase}";

            // HttpContext.Session.SetInt32("Fullness", ViewBag.Fullness);
            return RedirectToAction("Index");
        }

        [HttpGet("/play")]
        public IActionResult Play()
        {
            Random rand = new Random();
            int increase = rand.Next(5,10);
            int? Happiness = HttpContext.Session.GetInt32("Happiness") + increase;
            HttpContext.Session.SetInt32("Happiness", (int) Happiness);
            //Happiness+=increase;
            int? Energy = HttpContext.Session.GetInt32("Energy") - 5;
            HttpContext.Session.SetInt32("Energy", (int) Energy);
            TempData["message"] = $"You played with your dojodachi, Energy -5, Happiness {increase} "; 
            return RedirectToAction("Index");   
        }

        [HttpGet("/work")]
        public IActionResult Work()
        {   
            Random rand = new Random();
            int increase = rand.Next(1,3);
            int? Meals = HttpContext.Session.GetInt32("Meals") + increase;
            HttpContext.Session.SetInt32("Meals", (int) Meals);
            int? Energy = HttpContext.Session.GetInt32("Energy") - 5;
            HttpContext.Session.SetInt32("Energy", (int) Energy);
            TempData["message"] = $"You worked with your dojodachi, Meals {increase} , Energy - 5 ";
        
            return RedirectToAction("Index");
          }

        [HttpGet("/sleep")]
        public IActionResult Sleep()
        {
            //fullness-=5;
            int? Fullness = HttpContext.Session.GetInt32("Fullness") - 5;
            HttpContext.Session.SetInt32("Fullness", (int) Fullness);
            //energy+=15;
            int? Energy = HttpContext.Session.GetInt32("Energy") + 15;
            HttpContext.Session.SetInt32("Energy", (int) Energy);
            //happiness-=5;
            int? Happiness = HttpContext.Session.GetInt32("Happiness") - 5;
            HttpContext.Session.SetInt32("Happiness", (int) Happiness);
            TempData["message"] = "You slept with your dojodachi, Fullness -5, Energy +15, Happiness -5 ";
            return RedirectToAction("Index");
        }

        [HttpGet("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
