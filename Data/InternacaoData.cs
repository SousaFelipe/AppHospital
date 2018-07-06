using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using Model;


namespace Data
{
    public class InternacaoData : Conexao
    {
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



        private class Content
        {
            public Internacao Get(MySqlDataReader reader)
            {
                Internacao internacao = new Internacao
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Paciente = Convert.ToInt32(reader["paciente"]),
                    Causa = Convert.ToString(reader["causa"]),
                    Leito = Convert.ToInt32(reader["leito"]),
                    DataEntrada = Convert.ToDateTime(reader["data_entrada"]),
                    DataSaida = Convert.ToDateTime(reader["data_saida"])
                };

                return internacao;
            }
        }
    }
}
