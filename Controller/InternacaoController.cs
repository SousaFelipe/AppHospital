using System.Collections.Generic;

using Data;
using Model;


namespace Controller
{
    public class InternacaoController
    {
        public bool PacienteInternado(int id)
        {
            return new InternacaoData().PacienteInternado(id);
        }



        public List<Internacao> Listar(int id)
        {
            return new InternacaoData().Listar(id);
        }



        public Internacao Buscar(string[] colunas, string[] valores)
        {
            return new InternacaoData().Buscar(colunas, valores);
        }



        public string Inserir(Internacao internacao)
        {
            if (internacao.ID <= 0)
            {
                return new InternacaoData().Inserir(internacao);
            }
            else
            {
                return new InternacaoData().Atualizar(internacao);
            }
        }
    }
}