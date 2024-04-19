using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace web_estudos.Repositories.SQLServer
{
    public class Paciente
    {
        private readonly string connectionString;
        public Paciente(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Models.Paciente> Get(string connectionString)
        {
            List<Models.Paciente> pacientes = new List<Models.Paciente>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "select codigo, nome, datanascimento from paciente"; //comando SQL usado
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
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
    }
}