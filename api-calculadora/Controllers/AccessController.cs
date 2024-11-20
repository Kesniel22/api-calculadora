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

        [HttpGet]
        [Route("api/filtrarCalculos")]
        public IHttpActionResult filtrarCalculos(string operacion)
        {
            try
            {
                var resultados = obtenerCalculos.filterCalcs(operacion);

                if (resultados == null || resultados.Count == 0)
                {
                    return NotFound(); // Respuesta HTTP 404.
                }
                return Ok(resultados); // Respuesta HTTP 200 con los datos.
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Respuesta HTTP 400 por errores de parametros.
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Respuesta HTTP 500 Error del servidor.
            }
        }
    }
}
