using System;
using System.Windows.Controls;


namespace View.Adapter
{
    public abstract class StackPanelAdapter
    {
        public StackPanel Container { get; private set; }



        protected StackPanelAdapter(StackPanel container)
        {
            if (container != null)
            {
                Container = container;
            }
            else
            {
                throw new Exception("O objeto do tipo 'StackPanel' está nulo!");
            }
        }



        public abstract void Build();
    }
}
