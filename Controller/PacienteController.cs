using System.Collections.Generic;

using Data;
using Model;


namespace Controller
{
    public class PacienteController
    {
        public List<Paciente> ListarInternados()
        {
            return new PacienteData().Listar(Paciente.Listagem.Internados);
        }



        public List<Paciente> ListarTodos()
        {
            return new PacienteData().Listar(Paciente.Listagem.Todos);
        }
        


        public List<Paciente> Filtrar(string nome)
        {
            return new PacienteData().Filtar(nome);
        }



        public Paciente Buscar(string[] colunas, string[] valores)
        {
            return new PacienteData().Buscar(colunas, valores);
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
                    return (Buscar(new string[] { "id" }, new string[] { paciente.ID.ToString() }) != null)
                        ? new PacienteData().Atualizar(paciente)
                        : new PacienteData().Inserir(paciente);
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