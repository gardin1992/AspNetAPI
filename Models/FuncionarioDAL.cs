using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class FuncionarioDAL : IFuncionarioDAL
    {
        string connectionString = "Data Source=(local);Initial Catalog = CadastroDB; Integrated Security = True;";
        public void AddFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Funcionarios (Nome,Cidade,Departamento,Sexo) Values(@Nome, @Cidade, @Departamento, @Sexo)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteFuncionario(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Funcionarios where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            List<Funcionario> lstfuncionario = new List<Funcionario>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlString = "SELECT FuncionarioId, Nome,Cidade, Departamento,Sexo from Funcionarios";
                // cria o sql de comando com a conexão e com o sql
                SqlCommand cmd = new SqlCommand(sqlString, con);
                // seta o type de comando 
                cmd.CommandType = CommandType.Text;
                // abre a conexão
                con.Open();
                // Executa a conexão e cria um leitor de dados sql
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();
                    lstfuncionario.Add(funcionario);
                }
                con.Close();
            }

            return lstfuncionario;
        }

        public Funcionario GetFuncionario(int? id)
        {
            Funcionario funcionario = new Funcionario();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Funcionarios WHERE FuncionarioId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();
                }
            }
            return funcionario;
        }

        public void UpdateFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update Funcionarios set Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", funcionario.FuncionarioId);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
