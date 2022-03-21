using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Controllers
{
    public class SeriesClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Series()
        {
            return View();
        }

        public IActionResult PersonajesSerie()
        {
            return View();
        }

        public IActionResult InsertarSerie()
        {
            return View();
        }

        public IActionResult EliminarSerie()
        {
            return View();
        }
    }
}
