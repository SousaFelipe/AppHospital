using System.Collections.Generic;

using Data;
using Model;


namespace Controller
{
    public class InternacaoController
    {
        public List<Internacao> Listar(int id)
        {
            return new InternacaoData().Listar(id);
        }



        public bool PacienteInternado(int id)
        {
            return new InternacaoData().PacienteInternado(id);
        }
    }
}
