using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using Model;


namespace Data
{
    public class PacienteData : Conexao
    {
        public List<Paciente> Listar(int min, int max)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    string sql = "SELECT * FROM pacientes WHERE id BETWEEN '" + min + "' AND '" + max + "' ORDER BY id DESC";

                    using (MyCommand = new MySqlCommand(sql, MyConnection))
                    {
                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            List<Paciente> pacientes = new List<Paciente>();

                            while (MyReader.Read())
                            {
                                pacientes.Add(new Content().Get(MyReader));
                            }

                            return pacientes;
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



        public List<Paciente> Filtar(string nome)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("SELECT * FROM pacientes WHERE nome LIKE '" + nome + "%'", MyConnection))
                    {
                        using (MyReader = MyCommand.ExecuteReader())
                        {
                            List<Paciente> pacientes = new List<Paciente>();

                            while (MyReader.Read())
                            {
                                pacientes.Add(new Content().Get(MyReader));
                            }

                            return pacientes;
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



        public int Contar(Paciente.Contador contador)
        {
            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    string sql = (contador.Equals(Paciente.Contador.MaxID))
                        ? "SELECT MAX(id) as id FROM pacientes"
                        : "SELECT COUNT(*) FROM pacientes";

                    using (MyCommand = new MySqlCommand(sql, MyConnection))
                    {
                        if (contador.Equals(Paciente.Contador.MaxID))
                        {
                            using (MyReader = MyCommand.ExecuteReader())
                            {
                                if (MyReader.Read())
                                {
                                    return Convert.ToInt32(MyReader["id"]);
                                }
                            }
                        }
                        else if (contador.Equals(Paciente.Contador.Quantidade))
                        {
                            return Convert.ToInt32(MyCommand.ExecuteScalar());
                        }
                    }
                }
            }
            finally
            {
                MyConnection.Close();
                MyCommand.Dispose();

                if (MyReader != null && !MyReader.IsClosed)
                {
                    MyReader.Close();
                }
            }

            return -1; 
        }



        public string Inserir(Paciente paciente)
        {
            int id = Contar(Paciente.Contador.MaxID) + 1;

            try
            {
                using (MyConnection = new MySqlConnection(ConnectionString))
                {
                    MyConnection.Open();

                    using (MyCommand = new MySqlCommand("INSERT INTO pacientes (id, nome, responsavel, data_nascimento, sexo, cartao_sus, endereco," +
                        "telefone) VALUES (@id, @nome, @responsavel, @data_nascimento, @sexo, @cartao_sus, @endereco, @telefone)", MyConnection))
                    {
                        MyCommand.Parameters.AddWithValue("@id", id);
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
                MyConnection.Close();
                MyCommand.Dispose();
            }
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
                return "Erro ao remover paciente!";
            }
            finally
            {
                MyConnection.Close();
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
                MyConnection.Close();
                MyCommand.Dispose();
            }
        }



        private class Content
        {
            public Paciente Get(MySqlDataReader reader)
            {
                Paciente paciente = new Paciente
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Nome = Convert.ToString(reader["nome"]),
                    Responsavel = Convert.ToString(reader["responsavel"]),
                    DataNascimento = Convert.ToDateTime(reader["data_nascimento"]),
                    Sexo = Convert.ToInt32(reader["sexo"]),
                    CartaoSus = Convert.ToString(reader["cartao_sus"]),
                    Endereco = Convert.ToString(reader["endereco"]).Split(';'),
                    Telefone = Convert.ToString(reader["telefone"])
                };

                return paciente;
            }
        }
    }
}
