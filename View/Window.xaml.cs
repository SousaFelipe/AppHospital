using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using View.Adapter;
using View.Components.Dialogs;


namespace View
{
    public partial class Window : System.Windows.Window
    {
        private PacientePagesAdapter PagesAdapter { get; set; }



        public Window()
        {
            InitializeComponent();
            Single.SetMainWindow(this);
            Refresh();
        }



        public void Refresh()
        {
            PagesAdapter = new PacientePagesAdapter(stp_pages)
            {
                ViewAdapter = new PacienteViewAdapter(stp_pacientes)
            };

            PagesAdapter.Build();
        }



        private void FabNovoPaciente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(245, 0, 87));
        }



        private void FabNovoPaciente_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fab_novo_paciente.Background = new SolidColorBrush(Color.FromRgb(255, 64, 129));
            new NovoPacienteDialog().Show();
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