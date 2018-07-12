using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using Model;


namespace Data
{
    public class InternacaoData : Conexao
    {
        public bool PacienteInternado(int paciente)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("SELECT * FROM internacoes WHERE paciente=@paciente AND data_saida='" +
                        DateTime.MinValue.ToString("yyyy-MM-dd") + "'", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@paciente", paciente);

                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            List<Internacao> internacoes = new List<Internacao>();

                            while (MyReader.Read())
                            {
                                internacoes.Add(new Content().Get(MyReader));
                            }

                            return (internacoes.Count > 0);
                        }
                    }
                }
            }
            finally
            {
                MyReader.Close();
                MyCommand.Dispose();
                MyConnection.Close();
            }
        }



        public List<Internacao> Listar(int paciente)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("SELECT * FROM internacoes WHERE paciente=@paciente", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@paciente", paciente);

                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            List<Internacao> internacoes = new List<Internacao>();

                            while (MyReader.Read())
                            {
                                internacoes.Add(new Content().Get(MyReader));
                            }

                            return internacoes;
                        }
                    }
                }
            }
            finally
            {
                MyReader.Close();
                MyCommand.Dispose();
                MyConnection.Close();
            }
        }



        public Internacao Buscar(string[] colunas, string[] valores)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand(RawSelect("internacoes", colunas, valores), MyConnection))
                    {
                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            if (MyReader.Read())
                            {
                                return new Content().Get(MyReader);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            finally
            {
                MyConnection.Close();
                MyCommand.Dispose();
                MyReader.Close();
            }
        }



        public string Inserir(Internacao internacao)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("INSERT INTO internacoes (paciente, causa, data_entrada, data_saida, nota) " +
                        "VALUES (@paciente, @causa, @data_entrada, @data_saida, @nota)", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@paciente", internacao.Paciente);
                        MyCommand.Parameters.AddWithValue("@causa", internacao.Causa);
                        MyCommand.Parameters.AddWithValue("@data_entrada", internacao.DataEntrada);
                        MyCommand.Parameters.AddWithValue("@data_saida", internacao.DataSaida);
                        MyCommand.Parameters.AddWithValue("@nota", internacao.Nota);

                        MyCommand.ExecuteNonQuery();

                        return ("Internação registrada com sucesso!");
                    }
                }
            }
            catch (MySqlException)
            {
                return ("Ocorreu um erro ao registrar a internação do paciente!\nPor favor, entre em contato com o suporte.");
            }
            finally
            {
                MyConnection.Close();
                MyCommand.Dispose();
            }
        }



        public string Atualizar(Internacao internacao)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("UPDATE internacoes SET paciente=@paciente, causa=@causa, data_entrada=@data_entrada, " +
                        "data_saida=@data_saida, nota=@nota WHERE id=@id", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@id", internacao.ID);
                        MyCommand.Parameters.AddWithValue("@paciente", internacao.Paciente);
                        MyCommand.Parameters.AddWithValue("@causa", internacao.Causa);
                        MyCommand.Parameters.AddWithValue("@data_entrada", internacao.DataEntrada);
                        MyCommand.Parameters.AddWithValue("@data_saida", internacao.DataSaida);
                        MyCommand.Parameters.AddWithValue("@nota", internacao.Nota);

                        MyCommand.ExecuteNonQuery();

                        return ("Alteração realizada com sucesso!");
                    }
                }
            }
            catch (MySqlException)
            {
                return ("Ocorreu um erro ao alterar os dados da internação!\nPor favor, entre em contato com o suporte.");
            }
            finally
            {
                MyConnection.Close();
                MyCommand.Dispose();
            }
        }



        private class Content
        {
            public Internacao Get(MySqlDataReader reader)
            {
                Internacao internacao = new Internacao
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Paciente = Convert.ToInt32(reader["paciente"]),
                    Causa = Convert.ToString(reader["causa"]).ToLower(),
                    DataEntrada = Convert.ToDateTime(reader["data_entrada"]),
                    DataSaida = (DBNull.Value.Equals(reader["data_saida"])) ? DateTime.MinValue : Convert.ToDateTime(reader["data_saida"]),
                    Nota = (DBNull.Value.Equals(reader["nota"])) ? string.Empty : Convert.ToString(reader["nota"])
                };

                return internacao;
            }
        }
    }
}
