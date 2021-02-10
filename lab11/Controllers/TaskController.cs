using lab11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab11.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Task1()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Task1(Algorithms alg)
        {
           StartData sd = new StartData();
            alg = new Algorithms(sd.array1());
            
            ViewBag.Result = alg.CascadeSummationScheme();
            return View(alg);
        }
        [HttpGet]
        public IActionResult Task2()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Task2(Algorithms alg)
        {
            StartData sd = new StartData();
            alg = new Algorithms(sd.array2());

            ViewBag.Result = alg.ModifyCascadeSummationScheme();
            return View(alg);
        }
        [HttpGet]
        public IActionResult Task3()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Task3(Algorithms alg)
        {
            StartData sd = new StartData();
            alg = new Algorithms();

            ViewBag.ResultList = alg.LinearReduction(4);
            return View(alg);
        }
    }
}
