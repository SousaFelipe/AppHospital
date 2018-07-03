using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;
using View.Adapter;


namespace View.Components
{
    public partial class PacienteItemView : UserControl
    {
        public PacienteViewAdapter  Adapter { get; private set; }
        public Paciente             Item { get; set; }



        public PacienteItemView(PacienteViewAdapter adapter)
        {
            InitializeComponent();

            Adapter = adapter;

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
                grd_controles.Visibility = Visibility.Visible;
            }
            else
            {
                Background = null;
                grd_controles.Visibility = Visibility.Hidden;
            }
        }



        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Item = new PacienteController().Buscar(new string[] { "nome" }, new string[] { tbk_nome.Text });
        }
    }
}
