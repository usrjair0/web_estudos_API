using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using web_estudos.Models;
namespace web_estudos.Controllers
{
    public class PacientesController : ApiController
    {
        // GET: api/Pacientes
        public List<Models.Paciente> Get()
        {
            List<Models.Paciente> pacientes = new List<Models.Paciente>();

            using (SqlConnection conn = new SqlConnection())
            {
                string connectionString = @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
                conn.ConnectionString = connectionString;
                conn.Open();

                string sql = "select codigo, nome, datanascimento from paciente"; //comando SQL usado

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    using (SqlDataReader dr = cmd.ExecuteReader()) 
                    { 
                        while(dr.Read())
                        {
                            Models.Paciente paciente = new Models.Paciente();
                            paciente.Codigo = (int)dr["codigo"];
                            paciente.Nome = (string)dr["nome"];
                            paciente.DataNascimento = (DateTime)dr["datanascimento"];
                            pacientes.Add(paciente);
                        };
                    }
                }
            };

            return pacientes;
        }

        // GET: api/Pacientes/5
        public IHttpActionResult Get(int id)
        {
            Models.Paciente paciente = new Paciente();
            string connectionString = @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"select codigo, nome, datanascimento from paciente where codigo = {id};";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    SqlDataReader retorno = cmd.ExecuteReader();
                    if (retorno.Read())
                    {
                        paciente.Codigo = (int)retorno["codigo"];
                        paciente.Nome = (string)retorno["nome"];
                        paciente.DataNascimento = (DateTime)retorno["datanascimento"];
                    }
                    else
                        return NotFound();
                }
            }

            return Ok(paciente);
        }

        // POST: api/Pacientes
        public IHttpActionResult Post(Models.Paciente paciente)
        {
            string connectionString = @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"insert into paciente (nome, datanascimento) values ('{paciente.Nome}', " +
                    $"'{paciente.DataNascimento}'); SELECT convert(int, @@IDENTITY)";
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    paciente.Codigo = (int)cmd.ExecuteScalar();
                }
            }
            if (paciente.Codigo == 0)
                return InternalServerError();
            else
                return Content(HttpStatusCode.Created, paciente);
        }
        // PUT: api/Pacientes/5
        public IHttpActionResult Put(int id, Models.Paciente paciente)
        {
            string connectionString = @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = $"update paciente set nome = '{paciente.Nome}' where codigo = {id};";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                }
            }
            return Content(HttpStatusCode.OK, paciente);
        }

        // DELETE: api/Pacientes/5
        public IHttpActionResult Delete(int id)
        {
            string connectionString = @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
            int linhas;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                string sql = $"Delete From paciente where codigo = {id};";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    linhas = cmd.ExecuteNonQuery();
                }
            };
            if (linhas > 0)
                return Content(HttpStatusCode.OK, "Registro excluído com sucesso.");
            else
                return BadRequest("Deu errado");
        }
    }
}
