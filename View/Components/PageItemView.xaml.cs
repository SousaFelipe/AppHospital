using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace View.Components
{
    public partial class PageItemView : UserControl
    {
        public int[] Range { get; private set; }



        public PageItemView()
        {
            InitializeComponent();
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
        }



        public void Reject()
        {
            btn.Background = new SolidColorBrush(Colors.Transparent);
            txt_posicao.Foreground = new SolidColorBrush(Color.FromRgb(33, 33, 33));
            txt_posicao.FontWeight = FontWeights.Normal;
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
