using System.Collections.Generic;

using Data;
using Model;


namespace Controller
{
    public class PacienteController
    {
        public Paciente Buscar(string[] colunas, string[] valores)
        {
            return new PacienteData().Buscar(colunas, valores);
        }



        public List<Paciente> Listar(string trecho)
        {
            if (string.IsNullOrEmpty(trecho))
            {
                return new PacienteData().ListarPorTrecho(trecho);
            }
            else
            {
                return new PacienteData().ListarComLimite(6);
            }
        }
    }
}