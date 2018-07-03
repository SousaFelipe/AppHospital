using System;
using System.Windows.Controls;


namespace View.Adapter
{
    public abstract class StackPanelAdapter
    {
        public Window       Owner { get; private set; }
        public StackPanel   Container { get; private set; }



        protected StackPanelAdapter(Window owner, StackPanel container)
        {
            Owner = owner;
            Container = container;
        }



        public abstract void Build();
    }
}
