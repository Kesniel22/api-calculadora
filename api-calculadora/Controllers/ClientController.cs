using api_calculadora.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace api_calculadora.Controllers
{
    public class ClientController : Controller
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44393/"),
            Timeout = TimeSpan.FromSeconds(30)
        };
        // GET: Client

        public ClientController()
        {}
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ObtenerValores()
        {
            var response = await _client.GetAsync("/api/GetValues");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var resultados = JsonConvert.DeserializeObject<List<Historial_Calculos>>(data);
                ViewBag.Resultados = resultados;
            }
            else
            {
                ViewBag.Error = "Hubo un error al obtener los datos.";
            }
            return View();
        }

        public async Task<ActionResult> FiltrarCalculos(string operacion)
        {
            var response = await _client.GetAsync($"/api/filtrarCalculos?operacion={operacion}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var resultados = JsonConvert.DeserializeObject<List<Historial_Calculos>>(data);
                ViewBag.Resultados = resultados;
            }
            else
            {
                ViewBag.Error = "Hubo un error al filtrar los datos.";
            }
            return View();
        }
    }
}