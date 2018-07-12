using System.Collections.Generic;
using System.Windows.Controls;

using Model;
using View.Components.ItemViews;


namespace View.Adapter
{
    public class InternacaoViewAdapter : StackPanelAdapter
    {
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
                        itemView.ShowItem(internacao);

                        Container.Children.Add(itemView);
                    }
                }
            }
        }
    }
}