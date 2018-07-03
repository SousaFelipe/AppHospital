using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using Model;


namespace Data
{
    public class PacienteData : Conexao
    {
        public List<Paciente> ListarPorTrecho(string trecho)
        {
            using (MyConnection = new MySqlConnection(ConnectionString))
            {
                MyConnection.Open();

                using (MyCommand = new MySqlCommand("SELECT * FROM pacientes WHERE nome LIKE '" + trecho + "%'", MyConnection))
                {
                    using (MyReader = MyCommand.ExecuteReader())
                    {
                        List<Paciente> pacientes = new List<Paciente>();

                        while (MyReader.Read())
                        {
                            pacientes.Add(new Content().Get(MyReader));
                        }

                        MyReader.Close();
                        MyCommand.Dispose();
                        MyConnection.Close();

                        return pacientes;
                    }
                }
            }
        }



        public List<Paciente> ListarComLimite(int limite)
        {
            using (MyConnection = new MySqlConnection(ConnectionString))
            {
                MyConnection.Open();

                using (MyCommand = new MySqlCommand("SELECT * FROM pacientes ORDER BY nome ASC LIMIT @limite", MyConnection))
                {
                    MyCommand.Parameters.AddWithValue("@limite", limite);

                    using (MyReader = MyCommand.ExecuteReader())
                    {
                        List<Paciente> pacientes = new List<Paciente>();

                        while (MyReader.Read())
                        {
                            pacientes.Add(new Content().Get(MyReader));
                        }

                        MyConnection.Close();
                        MyCommand.Dispose();
                        MyReader.Close();

                        return pacientes;
                    }
                }
            }
        }



        public string Inserir(Paciente paciente)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("INSERT INTO pacientes (nome, responsavel, data_nascimento, sexo, cartao_sus, endereco," +
                        "telefone) VALUES (@nome, @responsavel, @data_nascimento, @sexo, @cartao_sus, @endereco, @telefone)", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@nome", paciente.Nome);
                        MyCommand.Parameters.AddWithValue("@responsavel", paciente.Responsavel);
                        MyCommand.Parameters.AddWithValue("@data_nascimento", paciente.DataNascimento);
                        MyCommand.Parameters.AddWithValue("@sexo", paciente.Sexo);
                        MyCommand.Parameters.AddWithValue("@cartao_sus", paciente.CartaoSus);
                        MyCommand.Parameters.AddWithValue("@endereco", string.Join(";", paciente.Endereco));
                        MyCommand.Parameters.AddWithValue("@telefone", paciente.Telefone);

                        MyCommand.ExecuteNonQuery();

                        return "Paciente inserido com sucesso!";
                    }
                }
            }
            catch (MySqlException)
            {
                return "Erro ao inserir cliente!";
            }
            finally
            {
                MyConnection.Clone();
                MyCommand.Dispose();
            }
        }



        public Paciente Buscar(string[] colunas, string[] valores)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand(RawSelect("pacientes", colunas, valores), MyConnection))
                    {
                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            if (MyReader.Read())
                            {
                                return new Content().Get(MyReader);
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

            return null;
        }



        public string Atualizar(Paciente paciente)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("UPDATE pacientes SET nome=@nome, responsavel=@responsavel, data_nascimento=@data_nascimento," +
                        "sexo=@sexo, cartao_sus=@cartao_sus, endereco=@endereco, telefone=@telefone  WHERE id=@id", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@id", paciente.ID);
                        MyCommand.Parameters.AddWithValue("@nome", paciente.Nome);
                        MyCommand.Parameters.AddWithValue("@responsavel", paciente.Responsavel);
                        MyCommand.Parameters.AddWithValue("@data_nascimento", paciente.DataNascimento);
                        MyCommand.Parameters.AddWithValue("@sexo", paciente.Sexo);
                        MyCommand.Parameters.AddWithValue("@cartao_sus", paciente.CartaoSus);
                        MyCommand.Parameters.AddWithValue("@endereco", string.Join(";", paciente.Endereco));
                        MyCommand.Parameters.AddWithValue("@telefone", paciente.Telefone);

                        MyCommand.ExecuteNonQuery();

                        return "Paciente atualizado com sucesso!";
                    }
                }
            }
            catch (MySqlException)
            {
                return "Erro ao remover cliente!";
            }
            finally
            {
                MyConnection.Clone();
                MyCommand.Dispose();
            }
        }



        public string Remover(int id)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("DELETE FROM pacientes WHERE id=@id", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@id", id.ToString());

                        MyCommand.ExecuteNonQuery();

                        return "Paciente removido com sucesso!";
                    }
                }
            }
            catch (MySqlException)
            {
                return "Erro ao  remover paciente!";
            }
            finally
            {
                MyConnection.Clone();
                MyCommand.Dispose();
            }
        }



        private class Content
        {
            public Paciente Get(MySqlDataReader reader)
            {
                Paciente paciente = new Paciente();

                paciente.ID = Convert.ToInt32(reader["id"]);
                paciente.Nome = Convert.ToString(reader["nome"]);
                paciente.Responsavel = Convert.ToString(reader["responsavel"]);
                paciente.DataNascimento = Convert.ToDateTime(reader["data_nascimento"]);
                paciente.Sexo = Convert.ToInt32(reader["sexo"]);
                paciente.CartaoSus = Convert.ToString(reader["cartao_sus"]);
                paciente.Endereco = Convert.ToString(reader["endereco"]).Split(';');
                paciente.Telefone = Convert.ToString(reader["telefone"]);

                return paciente;
            }
        }
    }
}
