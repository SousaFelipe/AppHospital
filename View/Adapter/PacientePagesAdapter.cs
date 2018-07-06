using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using Controller;
using Model;
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
                int count = (Owner.cbx_modo.SelectedIndex <= 0)
                    ? new PacienteController().ContarPermanentes()
                    : new PacienteController().ContarTodos();

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
            PageItemView current = Current();

            if (current != null && current.Index > 0)
            {
                Load(current.Index - 1);
            }
        }



        public void Next()
        {
            PageItemView current = Current();

            if (current != null && current.Index < (Container.Children.Count - 1))
            {
                Load(current.Index + 1);
            }
        }



        public void Load(int index)
        {
            PageItemView itemView = ((PageItemView)Container.Children[index]);

            ViewAdapter.Dataset = (Owner.cbx_modo.SelectedIndex <= 0)
                ? new PacienteController().ListarPermanentes(itemView.Range[0], itemView.Range[1])
                : new PacienteController().ListarTodos(itemView.Range[0], itemView.Range[1]);

            ViewAdapter.Build();

            Current().Reject();
            itemView.Select();
        }



        public override void Build()
        {
            if (Container != null)
            {
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

                Single.MainWindow.grd_pacientes.Visibility = (Container.Children.Count <= 0) ? Visibility.Hidden : Visibility.Visible;
                Single.MainWindow.tbk_pacientes.Visibility = (Container.Children.Count <= 0) ? Visibility.Visible : Visibility.Hidden;

                if (Container.Children.Count > 0)
                {
                    Load(0);
                }
            }
        }



        private void BuildRange()
        {
            if (Single.MainWindow.cbx_modo.SelectedIndex <= 0)
            {
                int[] ids = null;
                PacienteController controller = new PacienteController();
                List<Paciente> pacientes = controller.ListarPermanentes(1, controller.ContarTodos());

                if (pacientes != null)
                {
                    ids = new int[pacientes.Count];

                    for (int i = 0; i < pacientes.Count; i++)
                    {
                        ids[i] = pacientes[i].ID;
                    }
                }

                int count  = 0;
                for (int i = 0; count < PageCount; i += PATIENTS_PER_PAGE)
                {
                    int max = ((i + PATIENTS_PER_PAGE) - 1);

                    ((PageItemView)Container.Children[count]).SetRange(
                        ids[i],
                        ids[(max > PageCount) ? PageCount : max]
                    );

                    count++;
                }
            }
            else
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    ((PageItemView)Container.Children[i - 1]).SetRange((PATIENTS_PER_PAGE * i) - 5, (PATIENTS_PER_PAGE * i));
                }
            }
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
    }
}
