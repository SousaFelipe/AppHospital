using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using View.Adapter;


namespace View.Components
{
    public partial class PageItemView : UserControl
    {
        public PacientePagesAdapter Adapter     { get; private set; }
        public int[]                Range       { get; private set; }
        public bool                 Selected    { get; private set; }
        public int                  Index       { get; set; }



        public PageItemView(PacientePagesAdapter adapter)
        {
            InitializeComponent();
            Adapter = adapter;
            Range = new int[2];
        }



        public void SetRange(int min, int max)
        {
            Range[0] = min;
            Range[1] = max;
        }



        public void Select()
        {
            btn.Background = new SolidColorBrush(Color.FromRgb(255, 64, 129));
            txt_posicao.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            txt_posicao.FontWeight = FontWeights.Bold;
            Selected = true;
        }



        public void Reject()
        {
            btn.Background = new SolidColorBrush(Colors.Transparent);
            txt_posicao.Foreground = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            txt_posicao.FontWeight = FontWeights.Normal;
            Selected = false;
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!Selected)
            {
                Adapter.Load(Index);
            }
        }
    }
}
