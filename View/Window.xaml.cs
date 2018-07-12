using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;

using Model;
using Controller;
using View.Adapter;
using View.Components;
using View.Components.Dialogs;


namespace View
{
    public partial class Window : System.Windows.Window
    {
        private Pagination PageSystem { get; set; }



        public Window()
        {
            InitializeComponent();

            PageSystem = new Pagination(this)
            {
                ViewAdapter = new PacienteViewAdapter(stp_pacientes),
                PagesAdapter = new PacientePagesAdapter(stp_pages),
            };

            PageSystem.ViewAdapter.Owner = PageSystem;
            PageSystem.PagesAdapter.Owner = PageSystem;

            Refresh(string.Empty);
        }



        public void Refresh(string search)
        {
            List<Paciente> pacientes = new List<Paciente>();

            if (string.IsNullOrEmpty(search))
            {
                pacientes = (cbx_modo.SelectedIndex <= 0)
                    ? new PacienteController().ListarInternados()
                    : new PacienteController().ListarTodos();
            }
            else
            {
                pacientes = new PacienteController().Filtrar(search);
            }
            
            if (pacientes.Count > 0)
            {
                PageSystem.Update(pacientes);
            }

            grd_pacientes.Visibility = (pacientes.Count > 0) ? Visibility.Visible : Visibility.Hidden;
            tbk_pacientes.Visibility = (pacientes.Count > 0) ? Visibility.Hidden : Visibility.Visible;
        }



        private void FabNovoPaciente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(245, 0, 87));
        }



        private void FabNovoPaciente_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(255, 64, 129));
            new NovoPacienteDialog(this).Show();
        }



        private void PreviousItem_Click(object sender, RoutedEventArgs e)
        {
            PageSystem.Previous();
        }



        private void NextItem_Click(object sender, RoutedEventArgs e)
        {
            PageSystem.Next();
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageSystem != null)
            {
                Refresh(string.Empty);
            }
        }



        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Name.Equals("search"))
            {
                txb_pesquisa.Visibility = Visibility.Visible;
                txb_pesquisa.Focus();

                menu_open_search.Visibility = Visibility.Hidden;
                menu_close_search.Visibility = Visibility.Visible;
            }
            else if (((MenuItem)sender).Name.Equals("close"))
            {
                txb_pesquisa.Visibility = Visibility.Hidden;
                txb_pesquisa.Text = string.Empty;

                menu_close_search.Visibility = Visibility.Hidden;
                menu_open_search.Visibility = Visibility.Visible;
            }
        }



        private void TxbPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh(txb_pesquisa.Text);
        }
    }
}