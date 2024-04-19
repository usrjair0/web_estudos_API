using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using web_estudos.Models;
namespace web_estudos.Controllers
{
    public class PacientesController : ApiController
    {
        private readonly string connectionString;

        public PacientesController()
        {
            this.connectionString =
            @"server=DESKTOP-FKJDNP8\SQLEXPRESS;Database=consultorioMY;Trusted_Connection = True;";
        }

        // GET: api/Pacientes
        public List<Models.Paciente> Get()
        {
            List<Models.Paciente> pacientes = new List<Models.Paciente>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
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
            using (SqlConnection conn = new SqlConnection(this.connectionString))
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
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "insert into paciente (nome, datanascimento) values (@nome, " +
                    "@datanascimento); SELECT convert(int, @@IDENTITY) as codigo"; 
                    cmd.Connection = conn;
                    //SEGURANÇA IMPLEMENTADO NA AULA 30.
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = paciente.Nome;
                    cmd.Parameters.Add(new SqlParameter("@datanascimento", System.Data.SqlDbType.Date)).Value = paciente.DataNascimento;
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
            if (id == paciente.Codigo)
                return BadRequest("O id da requisição não coincide com o código do paciente");

            int linhasAfetadas;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open(); 
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"update paciente set nome = '{paciente.Nome}', " +
                    $"datanascimento = '{paciente.DataNascimento.ToString("yyyy-MM-dd")}' where codigo = {id};";
                    cmd.Connection = conn;
                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            if (linhasAfetadas == 0)
                return NotFound();

            return Content(HttpStatusCode.OK, paciente);
        }

        // DELETE: api/Pacientes/5
        public IHttpActionResult Delete(int id)
        {
            int linhas;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
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
