using Controller;
using System.Windows.Media;

using View.Adapter;
using View.Components;


namespace View
{
    public partial class Window : System.Windows.Window
    {
        private PacienteViewAdapter Adapter { get; set; }



        public Window()
        {
            InitializeComponent();
            Refresh(null);
        }



        public void Refresh(string trecho)
        {
            Adapter = new PacienteViewAdapter(this, stp_pacientes)
            {
                Dataset = new PacienteController().Listar(null)
            };

            Adapter.Build();
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
    }
}