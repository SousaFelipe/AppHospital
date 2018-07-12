using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;
using View.Adapter;


namespace View.Components.ItemViews
{
    public partial class InternacaoItemView : UserControl
    {
        private InternacaoViewAdapter   Owner { get; set; }
        private Internacao              Item  { get; set; }



        public InternacaoItemView(InternacaoViewAdapter owner)
        {
            InitializeComponent();
            Owner = owner;

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
                grd_ctrl.Visibility = Visibility.Visible;
            }
            else
            {
                Background = new SolidColorBrush(Colors.Transparent);
                grd_ctrl.Visibility = Visibility.Hidden;
            }
        }


        public void ShowItem(Internacao internacao)
        {
            Item = internacao;

            tbk_causa.Text = Item.Causa[0].ToString().ToUpper() + Item.Causa.Substring(1);
            tbk_entrada.Text = Item.DataEntrada.ToString("dd/MM/yyyy");
            tbk_saida.Text = (Item.DataSaida.Equals(DateTime.MinValue)) ? "-- -- ----" : Item.DataSaida.ToString("dd/MM/yyyy");
        }



        private void Delete()
        {
            MessageBoxResult result = MessageBox.Show(
                "Você realmente deseja remover este registro?\nEsta ação é ireversível!",
                "Removendo internação...",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No
            );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(new PacienteController().Remover(Item.ID), "Removendo paciente...", MessageBoxButton.OK, MessageBoxImage.Information);
                Owner.Build();
            }
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            if (item.Name.Equals("edit"))
            {
                
            }
            else if (item.Name.Equals("delete"))
            {
                Delete();
            }
        }



        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string msg = (string.IsNullOrEmpty(Item.Nota)) ? "Nenhuma nota!" : Item.Nota;
            msg = (msg[0].ToString().ToUpper() + msg.Substring(1));
            MessageBox.Show(msg, "Bloco de notas", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}