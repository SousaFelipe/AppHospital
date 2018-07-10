using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;


namespace View.Components.ItemViews
{
    public partial class InternacaoItemView : UserControl
    {
        private Internacao Item { get; set; }



        public InternacaoItemView(Internacao internacao)
        {
            InitializeComponent();
            Item = internacao;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MouseObserver);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }



        private void MouseObserver(object sender, EventArgs e)
        {
            double x = Mouse.GetPosition(this).X;
            double y = Mouse.GetPosition(this).Y;

            if ((x > 0 && x < ActualWidth) && (y > 0 && y < ActualHeight))
            {
                Background = new SolidColorBrush(Color.FromRgb(224, 247, 250));
                grd_note.Visibility = Visibility.Visible;
            }
            else
            {
                Background = new SolidColorBrush(Colors.Transparent);
                grd_note.Visibility = Visibility.Hidden;
            }
        }


        public void ShowItem()
        {
            tbk_leito.Text = Item.Leito.ToString();
            tbk_causa.Text = Item.Causa[0].ToString().ToUpper() + Item.Causa.Substring(1);
            tbk_entrada.Text = Item.DataEntrada.ToString("dd/MM/yyyy");
            tbk_saida.Text = (Item.DataSaida.Equals(DateTime.MinValue)) ? "-- -- ----" : Item.DataSaida.ToString("dd/MM/yyyy");
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string msg = (string.IsNullOrEmpty(Item.Nota)) ? "Nenhuma nota!" : Item.Nota;
            msg = (msg[0].ToString().ToUpper() + msg.Substring(1));
            MessageBox.Show(msg, "Bloco de notas", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}