using System.Windows.Controls;


namespace View.Adapter
{
    public class Pagination
    {
        public Window       Owner        { get; set; }
        public StackPanel   ContentItens { get; set; }
        public StackPanel   ContentPages { get; set; }



        public Pagination(StackPanel contentItens, StackPanel contentPages)
        {
            ContentItens = contentItens;
            ContentPages = contentPages;
        }



        public void Build()
        {

        }
    }
}
