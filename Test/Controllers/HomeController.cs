using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;

namespace Test.Controllers
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
            return View();
        }

        public JsonResult ContarPalabras(string text)
        {
            try
            {
                if (String.IsNullOrEmpty(text)) throw new Exception("No ingresó ninguna palabra");

                List<string> palabras = text.Split(' ').ToList();

                Dictionary<string, int> diccionarioPalabras = new Dictionary<string, int>();

                foreach (string palabra in palabras)
                {
                    if (diccionarioPalabras.ContainsKey(palabra))
                    {
                        diccionarioPalabras[palabra]++;
                    }
                    else
                    {
                        diccionarioPalabras.Add(palabra, 1);
                    }
                }

                var result = diccionarioPalabras.Select(kvp => new PalabrasModel { Nombre = kvp.Key, Cantidad = kvp.Value }).ToList() ;

                return Json(new {status = 200, success = true, data = result});

            } 
            catch (Exception ex)
            {
                return Json(new { status = 400, success = false, message = ex.Message });
            }

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