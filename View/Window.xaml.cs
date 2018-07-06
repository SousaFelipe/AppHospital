using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using View.Adapter;
using View.Components;


namespace View
{
    public partial class Window : System.Windows.Window
    {
        private Pagination PageSystem { get; set; }

        private PacienteViewAdapter  ViewAdapter { get; set; }
        private PacientePagesAdapter PagesAdapter { get; set; }



        public Window()
        {
            InitializeComponent();
            Single.SetMainWindow(this);
            Refresh();
        }



        public void Refresh()
        {
            PageSystem = new Pagination(stp_pacientes, stp_pages)
            {
                Owner = this
            };

            ViewAdapter = new PacienteViewAdapter(this, stp_pacientes);
            PagesAdapter = new PacientePagesAdapter(this, stp_pages, ViewAdapter);
            PagesAdapter.Build();
        }



        private void FabNovoPaciente_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(245, 0, 87));
        }



        private void FabNovoPaciente_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(255, 64, 129));
            new NovoPacienteDialog(this).Show();
        }



        private void PreviousItem_Click(object sender, RoutedEventArgs e)
        {
            PagesAdapter.Previous();
        }



        private void NextItem_Click(object sender, RoutedEventArgs e)
        {
            PagesAdapter.Next();
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}