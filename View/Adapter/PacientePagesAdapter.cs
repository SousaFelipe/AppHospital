using System;
using System.Windows.Controls;

using Model;
using Controller;
using View.Components;


namespace View.Adapter
{
    public class PacientePagesAdapter : StackPanelAdapter
    {
        
        /// <summary>
        /// Quantidade de pacientes por página
        /// </summary>
        public const int PATIENTS_PER_PAGE = 6;



        private PacienteViewAdapter ViewAdapter { get; set; }



        public int PageCount
        {
            get
            {
                int count = new PacienteController().Contar(Paciente.Contador.Quantidade);
                decimal ceiling = Math.Ceiling((decimal)(count / PATIENTS_PER_PAGE));
                return Convert.ToInt32(ceiling) + ((ceiling * PATIENTS_PER_PAGE < count) ? 1 : 0);
            }
        }



        public PacientePagesAdapter(Window owner, StackPanel container, PacienteViewAdapter viewAdapter) : base(owner, container)
        {
            ViewAdapter = viewAdapter;
        }



        public void Previous()
        {
            int currentIndex = Current().Index;

            if (currentIndex > 0)
            {
                Load(currentIndex - 1);
            }
        }



        public void Next()
        {
            int currentIndex = Current().Index;

            if (currentIndex < (Container.Children.Count - 1))
            {
                Load(currentIndex + 1);
            }
        }



        public void Load(int index)
        {
            PageItemView itemView = ((PageItemView)Container.Children[index]);

            ViewAdapter.Dataset = new PacienteController().Listar(itemView.Range[0], itemView.Range[1]);
            ViewAdapter.Build();

            Current().Reject();
            itemView.Select();
        }



        public override void Build()
        {
            int count = (PageCount * PATIENTS_PER_PAGE);
            Container.Children.Clear();

            for (int i = 1; i <= PageCount; i++)
            {
                PageItemView itemView = new PageItemView(this)
                {
                    Index = (i - 1)
                };

                itemView.txt_posicao.Text = i.ToString();
                
                if (i == 1)
                {
                    itemView.Select();
                }

                Container.Children.Add(itemView);
            }

            BuildRange();
            Load(0);
        }



        private PageItemView Current()
        {
            for (int i = 0; i < Container.Children.Count; i++)
            {
                PageItemView itemView = ((PageItemView)Container.Children[i]);
                if (itemView.Selected) return itemView;
            }

            return null;
        }



        private int MoveRange(int range)
        {
            int cei = (Container.Children.Count * PATIENTS_PER_PAGE);
            int max = new PacienteController().Contar(Paciente.Contador.MaxID);
            int dif = cei - max;
            int val = range - dif;

            return (val < 1) ? 1 : val;
        }



        private void BuildRange()
        {
            int index = (Container.Children.Count - 1);
            
            for (int i = (index + 1); i > 0; i--)
            {
                ((PageItemView)Container.Children[index - (i - 1)]).SetRange(
                    MoveRange((PATIENTS_PER_PAGE * i) - 5), MoveRange(PATIENTS_PER_PAGE * i)
                );
            }
            // [0] (07, 12) <<>> ()
            // [] () <<>> ()
        }
    }
}
