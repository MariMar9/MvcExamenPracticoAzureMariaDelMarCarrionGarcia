using Microsoft.AspNetCore.Mvc;
using MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Models;
using MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Controllers
{
    public class SeriesServidorController : Controller
    {
        private SeriesService service;

        public SeriesServidorController(SeriesService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Series()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        public async Task<IActionResult> DetallesSerie(int idSerie)
        {
            Serie serie = await this.service.FindSerieAsync(idSerie);
            return View(serie);
        }

        public IActionResult PersonajesSerie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonajesSerie(int idSerie)
        {
            List<Personaje> series = await this.service.PersonajesSerieAsync(idSerie);
            return View(series);
        }

        public IActionResult CreatePersonaje()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(string nombre, string imagen, int idSerie)
        {
            await this.service.InsertarPersonajeAsync(nombre, imagen, idSerie);
            ViewBag.Mensaje = "Personaje creado.";
            return View();
        }

        public IActionResult UpdateSerie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSerie(int idSerie, string nombre, string imagen, double puntuacion, int anio)
        {
            await this.service.ModificarSerieAsync(idSerie, nombre, imagen, puntuacion, anio);
            ViewBag.Mensaje = "Serie modificada.";
            return View();
        }

        public IActionResult DeletePersonaje()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonaje(int idPersonaje)
        {
            await this.service.EliminarPersonajeAsync(idPersonaje);
            ViewBag.Mensaje = "Personaje eliminado.";
            return View();
        }

        public async Task<IActionResult> MoverPersonajeSerie()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewBag.Personajes = personajes;
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> MoverPersonajeSerie(int idPersonaje, int idSerie)
        {
            await this.service.MoverPersonajeSerieAsync(idPersonaje, idSerie);
            ViewBag.Mensaje = "Personaje movido.";
            List<Serie> series = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewBag.Personajes = personajes;
            return View(series);
        }

    }
}
