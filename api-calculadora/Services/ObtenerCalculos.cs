using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using api_calculadora.Models;

namespace api_calculadora.Services
{

    public class ObtenerCalculos
    {
        private readonly string connectionString;

        public ObtenerCalculos()
        {
            connectionString = ConfigurationManager.ConnectionStrings["calculadora"].ConnectionString;
        }
        public List<Historial_Calculos> ObtenerTodos()
        {
            var lista = new List<Historial_Calculos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT expresion, resultado FROM Historial_Calculos", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Historial_Calculos
                                {
                                    expresion = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                    resultado = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los datos de la tabla.", ex);
                }
            }

            return lista;
        }

        public void filterCalcs()
        {
        }
    }
}