using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;
using View.Components.Dialogs;


namespace View.Components.ItemViews
{
    public partial class PacienteItemView : UserControl
    {
        public Paciente Item
        {
            get
            {
                return new PacienteController().Buscar(
                    new string[] { "nome", "responsavel" }, new string[] { tbk_nome.Text, tbk_responsavel.Text }
                );
            }
        }



        public PacienteItemView()
        {
            InitializeComponent();

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
            PacienteDialog dialog = new PacienteDialog();
            dialog.CarregarPaciente(Item.ID);
            dialog.Show();
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;


            if (item.Name.Equals("history"))
            {

            }
            else if (item.Name.Equals("edit"))
            {
                new NovoPacienteDialog().Show(Item);
            }
            else if (item.Name.Equals("delete"))
            {
                Delete();
            }
        }



        private void Delete()
        {
            MessageBoxResult result = MessageBox.Show(
                    "Você realmente deseja remover este Paciente?\nEsta ação é ireversível!",
                    "Paciente",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(new PacienteController().Remover(Item.ID), "Removendo paciente...", MessageBoxButton.OK, MessageBoxImage.Information);
                Single.MainWindow.Refresh();
            }
        }
    }
}
