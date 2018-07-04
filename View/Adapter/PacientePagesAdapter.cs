using System;
using System.Windows.Controls;

using Model;
using Controller;
using View.Components;


namespace View.Adapter
{
    class PacientePagesAdapter : StackPanelAdapter
    {
        private PacienteViewAdapter ViewAdapter { get; set; }



        public int PageCount
        {
            get
            {
                int count = new PacienteController().Contar(Paciente.Contador.Quantidade);
                decimal ceiling = Math.Ceiling((decimal)(count / 6));
                return Convert.ToInt32(ceiling) + ((ceiling * 6 < count) ? 1 : 0);
            }
        }



        public PacientePagesAdapter(Window owner, StackPanel container, PacienteViewAdapter viewAdapter) : base(owner, container)
        {
            ViewAdapter = viewAdapter;
        }



        public override void Build()
        {
            int count = PageCount * 6;
            Container.Children.Clear();

            for (int i = 1; i <= PageCount; i++)
            {
                PageItemView itemView = new PageItemView();
                
                if (i == 1)
                {
                    itemView.Select();
                }

                Container.Children.Add(itemView);
            }

            for (int i = Container.Children.Count; i > 0; i--)
            {
                ((PageItemView)Container.Children[i - 1]).SetRange(((6 * i) - 5), (6 * i));
            }

            ViewAdapter.Dataset = new PacienteController().Listar(1, 6);
        }
    }
}
