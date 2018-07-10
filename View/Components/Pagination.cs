using System.Collections.Generic;

using Model;
using View.Adapter;


namespace View.Components
{
    public class Pagination
    {
        public Window               Owner   { get; private set; }
        public List<Paciente>       Dataset { get; private set; }

        public PacienteViewAdapter  ViewAdapter  { get; set; }
        public PacientePagesAdapter PagesAdapter { get; set; }



        public Pagination(Window owner)
        {
            Owner = owner;
        }



        public void Next()
        {
            PagesAdapter.Next();
        }



        public void Previous()
        {
            PagesAdapter.Previous();
        }



        public void Update(List<Paciente> dataset)
        {
            Dataset = dataset;
            UpdateWithCount(Dataset.Count);
            UpdataWithRange(PagesAdapter.Current().Range);
        }



        public void UpdateWithCount(int count)
        {
            PagesAdapter.Update(count);
            PagesAdapter.Build();
        }



        public void UpdataWithRange(int[] range)
        {
            ViewAdapter.Update(Data(range));
            ViewAdapter.Build();
        }



        private List<Paciente> Data(int[] range)
        {
            List<Paciente> pacientes = new List<Paciente>();

            for (int i = range[0]; i <= range[range.Length - 1]; i++)
            {
                pacientes.Add(Dataset[i]);
            }

            return pacientes;
        }
    }
}