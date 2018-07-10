using System;
using System.Windows.Controls;

using View.Components;
using View.Components.ItemViews;


namespace View.Adapter
{
    public class PacientePagesAdapter : StackPanelAdapter
    {
        public const int PATIENTS_PER_PAGE = 6;
        
        public Pagination   Owner       { get; set; }
        public int          MaxRange    { get; private set; }
        public int          PageCount   { get; private set; }



        public PacientePagesAdapter(StackPanel container) : base(container) { }



        public void Update(int count)
        {
            decimal ceiling = Math.Ceiling((decimal)(count / PATIENTS_PER_PAGE));
            PageCount = Convert.ToInt32(ceiling) + ((ceiling * PATIENTS_PER_PAGE < count) ? 1 : 0);
            MaxRange = (count - 1);
        }



        public void UpdateByIndex(int index)
        {
            ClearSelection();

            PageItemView itemView = GetItem(index);
            itemView.Select();
            
            Owner.UpdataWithRange(itemView.Range);
        }



        public void Next()
        {
            PageItemView current = Current();
            PageItemView next = GetItem(current.Index + 1);

            if (next != null)
            {
                UpdateByIndex(next.Index);
            }
        }



        public void Previous()
        {
            PageItemView current = Current();
            PageItemView previous = GetItem(current.Index - 1);

            if (previous != null)
            {
                UpdateByIndex(previous.Index);
            }
        }



        public PageItemView Current()
        {
            if (Container != null && Container.Children.Count > 0)
            {
                for (int i = 0; i < Container.Children.Count; i++)
                {
                    PageItemView iv = ((PageItemView)Container.Children[i]);
                    if (iv.Selected) return iv;
                }
            }

            return null;
        }



        public PageItemView GetItem(int index)
        {
            int limit = (Container != null) ? (Container.Children.Count - 1) : -999;

            if (index >= 0 && index <= limit)
            {
                for (int i = 0; i <= limit; i++)
                {
                    PageItemView iv = ((PageItemView)Container.Children[i]);
                    if (iv.Index == index) return iv;
                }
            }

            return null;
        }



        public void ClearSelection()
        {
            if (Container != null && Container.Children.Count > 0)
            {
                for (int i = 0; i < Container.Children.Count; i++)
                {
                    PageItemView iv = ((PageItemView)Container.Children[i]);
                    if (iv.Selected) iv.Reject();
                }
            }
        }



        public override void Build()
        {
            if (Container != null)
            {
                Container.Children.Clear();

                for (int i = 0; i < PageCount; i++)
                {
                    PageItemView itemView = new PageItemView(this);
                    itemView.txt_posicao.Text = Convert.ToString(i + 1);
                    itemView.Index = i;

                    if (i == 0)
                    {
                        itemView.Select();
                    }

                    Container.Children.Add(itemView);
                }

                CreateRange();
            }
        }



        private void CreateRange()
        {
            int min = 0;
            int max = 0;

            for (int i = 1; i <= Container.Children.Count; i++)
            {
                min = ((PATIENTS_PER_PAGE * i) - PATIENTS_PER_PAGE);
                max = ((PATIENTS_PER_PAGE * i) - 1);

                if (max > MaxRange)
                {
                    int dif = (max - MaxRange);
                    max = (max - dif);
                }

                ((PageItemView)Container.Children[i - 1]).SetRange(min, max);
            }
        }
    }
}