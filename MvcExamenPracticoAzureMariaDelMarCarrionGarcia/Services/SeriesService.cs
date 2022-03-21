using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcExamenPracticoAzureMariaDelMarCarrionGarcia.Services
{
    public class SeriesService
    {
        private Uri UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public SeriesService(string url)
        {
            this.UrlApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> CallApiAsync<T>(string request)
        {
            using(HttpClient client=new HttpClient())
            {
                string url = "https://examenpracticoazuremariadelmarcarriongarcia2022.azurewebsites.net/";
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(url + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }


        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/Series";
            return await this.CallApiAsync<List<Serie>>(request);
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            return await this.CallApiAsync<List<Personaje>>(request);
        }

        public async Task<Serie> FindSerieAsync(int idSerie)
        {
            string request = "/api/Series/" + idSerie;
            return await this.CallApiAsync<Serie>(request);
        }

        public async Task<List<Personaje>> PersonajesSerieAsync(int idSerie)
        {
            string request = "/api/Series/PersonajesSerie/" + idSerie;
            return await this.CallApiAsync< List<Personaje>>(request);
        }

        public async Task InsertarPersonajeAsync(string nombre, string imagen, int idSerie)
        {
            using (HttpClient client=new HttpClient())
            {
                string request = "/api/Personajes";
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = 0;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idSerie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task ModificarSerieAsync(int idSerie, string nombre, string imagen, double puntuacion, int anio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series";
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.IdSerie = idSerie;
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Anio = anio;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task EliminarPersonajeAsync(int idPersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/" + idPersonaje;
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task MoverPersonajeSerieAsync(int idPersonaje, int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/MoverPersonajeSerie/" + idPersonaje + "/" + idSerie;
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                /*Personaje personaje = new Personaje()
                {
                    IdPersonaje = idPersonaje,
                    Nombre = "",
                    Imagen = "",
                    IdSerie = idSerie,
                };
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");*/
                HttpResponseMessage response = await client.PutAsync(request, null);
            }
        }
    }
}
