using System;
using System.Collections.Generic;
using System.Windows.Controls;

using Model;
using View.Components.ItemViews;


namespace View.Adapter
{
    public class InternacaoViewAdapter : StackPanelAdapter
    {
        public Window           Owner   { get; set; }
        public List<Internacao> Dataset { get; set; }



        public InternacaoViewAdapter(StackPanel container) : base(container) { }



        public override void Build()
        {
            if (Container != null)
            {
                Container.Children.Clear();

                if (Dataset != null)
                {
                    foreach (Internacao internacao in Dataset)
                    {
                        InternacaoItemView itemView = new InternacaoItemView(this);
                        itemView.hidden.Text = internacao.ID.ToString();
                        itemView.tbk_causa.Text = internacao.Causa[0].ToString().ToUpper() + internacao.Causa.Substring(1);
                        itemView.tbk_entrada.Text = internacao.DataEntrada.ToString("dd/MM/yyyy");

                        itemView.tbk_saida.Text = (internacao.DataSaida.Equals(DateTime.MinValue))
                            ? "-- -- ----"
                            : internacao.DataSaida.ToString("dd/MM/yyyy");

                        Container.Children.Add(itemView);
                    }
                }
            }
        }
    }
}