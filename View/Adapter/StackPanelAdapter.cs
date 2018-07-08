using System;
using System.Windows.Controls;


namespace View.Adapter
{
    public abstract class StackPanelAdapter
    {
        public StackPanel   Container { get; private set; }



        protected StackPanelAdapter(StackPanel container)
        {
            Container = container;
        }



        public abstract void Build();
    }
}
