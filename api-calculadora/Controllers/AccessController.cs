using api_calculadora.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using api_calculadora.Models;

namespace api_calculadora.Controllers
{
    public class AccessController : ApiController
    {
        private readonly ObtenerCalculos obtenerCalculos;

        public AccessController()
        {
            obtenerCalculos = new ObtenerCalculos();
        }

        [HttpGet]
        [Route("api/GetValues")]

        public IHttpActionResult Get()
        {
            try
            {
                List<Historial_Calculos> datos = obtenerCalculos.ObtenerTodos();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
