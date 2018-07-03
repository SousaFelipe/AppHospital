using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using Model;
using View.Components;


namespace View.Adapter
{
    public class PacienteViewAdapter : StackPanelAdapter
    {
        public List<Paciente> Dataset { get; set; }



        public PacienteViewAdapter(StackPanel container) : base(container) { }



        public override void Build()
        {
            if (Dataset != null)
            {
                Container.Children.Clear();

                foreach (Paciente paciente in Dataset)
                {
                    PacienteItemView itemView = new PacienteItemView(this);

                    itemView.icn_sexo.Source = (paciente.Sexo == 0)
                        ? new BitmapImage(new Uri(Path.Resources + "menina.png"))
                        : new BitmapImage(new Uri(Path.Resources + "menino.png"));

                    itemView.tbk_nome.Text = paciente.Nome;
                    itemView.tbk_responsavel.Text = paciente.Responsavel;
                    itemView.tbk_endereco.Text = paciente.Endereco[0];

                    Container.Children.Add(itemView);
                }
            }
        }
    }
}
