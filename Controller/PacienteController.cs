using System.Collections.Generic;

using Data;
using Model;


namespace Controller
{
    public class PacienteController
    {
        public List<Paciente> ListarPermanentes(int min, int max)
        {
            return new PacienteData().Listar(Paciente.Listagem.Permanentes, min, max);
        }



        public List<Paciente> ListarTodos(int min, int max)
        {
            return new PacienteData().Listar(Paciente.Listagem.Todos, min, max);
        }



        public List<Paciente> Filtrar(string nome)
        {
            return new PacienteData().Filtar(nome);
        }



        public Paciente Buscar(string[] colunas, string[] valores)
        {
            return new PacienteData().Buscar(colunas, valores);
        }



        public int ContarPermanentes()
        {
            return new PacienteData().Contar(Paciente.Listagem.Permanentes);
        }



        public int ContarTodos()
        {
            return new PacienteData().Contar(Paciente.Listagem.Todos);
        }



        public string Inserir(Paciente paciente)
        {
            if (paciente != null)
            {
                if (ExistemCamposVazios(paciente))
                {
                    return "Alguns campos importantes não foram preenchidos!";
                }
                else
                {
                    Paciente busca = Buscar(new string[] { "nome", "responsavel" }, new string[] { paciente.Nome, paciente.Responsavel });

                    if (busca != null)
                    {
                        paciente.ID = busca.ID;
                        return new PacienteData().Atualizar(paciente);
                    }
                    else
                    {
                        return new PacienteData().Inserir(paciente);
                    }
                }
            }

            return "O objeto do tipo \"Paciente\" está nulo!";
        }



        public string Remover(int id)
        {
            if (id > 0)
            {
                return new PacienteData().Remover(id);
            }

            return "Não foi possível encontrar esse paciente!";
        }



        public bool ExistemCamposVazios(Paciente paciente)
        {
            if (string.IsNullOrEmpty(paciente.Nome))
                return true;

            if (string.IsNullOrEmpty(paciente.DataNascimento.ToString("dd/MM/yyyy")))
                return true;

            if (string.IsNullOrEmpty(paciente.CartaoSus))
                return true;

            if (paciente.Sexo < 0)
                return true;

            if (string.IsNullOrEmpty(paciente.Responsavel))
                return true;

            if (paciente.Endereco != null)
            {
                if (string.IsNullOrEmpty(paciente.Endereco[0]))
                    return true;

                if (string.IsNullOrEmpty(paciente.Endereco[1]))
                    return true;
            }
            else
            {
                return true;
            }

            return false;
        }



        public static string Formatar(string unformated)
        {
            string formated = unformated;

            if (string.IsNullOrEmpty(formated))
            {
                return "Texto inválido!";
            }
            else
            {
                if (formated.Contains("/"))
                    formated = formated.Replace("/", "");

                if (formated.Contains("-"))
                    formated = formated.Replace("-", "");

                if (formated.Contains("."))
                    formated = formated.Replace(".", "");

                if (formated.Contains("("))
                    formated = formated.Replace("(", "");

                if (formated.Contains(")"))
                    formated = formated.Replace(")", "");

                if (formated.Contains(" "))
                    formated = formated.Replace(" ", "");

                return formated;
            }
        }
    }
}