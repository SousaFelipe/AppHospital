using Controller;
using View.Adapter;


namespace View
{
    public partial class Window : System.Windows.Window
    {
        private PacienteViewAdapter Adapter { get; set; }



        public Window()
        {
            InitializeComponent();

            Adapter = new PacienteViewAdapter(stp_pacientes)
            {
                Dataset = new PacienteController().Listar(null)
            };

            Adapter.Build();
        }
    }
}