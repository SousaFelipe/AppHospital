using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;


namespace View.Components.Dialogs
{
    public partial class NovoPacienteDialog : UserControl
    {
        private Window          Owner { get; set; }
        private DispatcherTimer Timer { get; set; }



        private Grid ContentOwner
        {
            get
            {
                return (Owner != null) ? ((Grid)Owner.Content) : null;
            }
        }



        public NovoPacienteDialog(Window owner)
        {   
            InitializeComponent();
            Owner = owner;
        }



        public void Show(Paciente paciente)
        {
            tbk_legenda.Text = "Editar paciente";

            txb_nome_paciente.Text = paciente.Nome;
            dpk_data_nascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            txb_cartao_sus.Text = string.Format("{0:### #### #### ####}", Convert.ToInt64(paciente.CartaoSus));
            cbx_sexo.SelectedIndex = paciente.Sexo + 1;
            txb_nome_responsavel.Text = paciente.Responsavel;
            txb_telefone_responsavel.Text = string.Format("{0:(##) # ####-####}", Convert.ToInt64(paciente.Telefone));
            txb_bairro_distrito.Text = paciente.Endereco[0];
            txb_rua.Text = paciente.Endereco[1];
            txb_numero_casa.Text = paciente.Endereco[2];

            AddToContent();
        }



        public void Show()
        {
            tbk_legenda.Text = "Novo paciente";
            AddToContent();
        }



        private void AddToContent()
        {
            int colCount = ContentOwner.ColumnDefinitions.Count;
            int rowCount = ContentOwner.RowDefinitions.Count;

            if (colCount > 0)
                Grid.SetColumnSpan(this, colCount);

            if (rowCount > 0)
                Grid.SetRowSpan(this, rowCount);

            ContentOwner.Children.Add(this);

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(_Show);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Start();
        }



        private void _Show(object sender, EventArgs e)
        {
            if (shadow.Opacity < 0.60F)
            {
                shadow.Opacity += 0.024F;
            }
            else
            {
                Timer.Stop();
                shadow.Opacity = 0.60F;
            }
        }



        public void Hide()
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(_Hide);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Start();
        }



        private void _Hide(object sender, EventArgs e)
        {
            if (shadow.Opacity > 0.00F)
            {
                shadow.Opacity -= 0.024F;
            }
            else
            {
                Timer.Stop();
                shadow.Opacity = 0.00F;
                ContentOwner.Children.Remove(this);
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);

            if (button.Content.Equals("SALVAR"))
            {
                Paciente paciente = new Paciente()
                {
                    Nome = txb_nome_paciente.Text,
                    DataNascimento = Convert.ToDateTime(dpk_data_nascimento.Text),
                    CartaoSus = PacienteController.Formatar(txb_cartao_sus.Text),
                    Sexo = (cbx_sexo.SelectedIndex - 1),
                    Responsavel = txb_nome_responsavel.Text,
                    Telefone = PacienteController.Formatar(txb_telefone_responsavel.Text),
                    Endereco = new string[] { txb_bairro_distrito.Text, txb_rua.Text, txb_numero_casa.Text }
                };

                Hide();

                MessageBox.Show(
                    new PacienteController().Inserir(paciente), "Salvando paciente...", MessageBoxButton.OK, MessageBoxImage.Information
                );

                Owner.Refresh(string.Empty);
            }
            else if (button.Content.Equals("CANCELAR"))
            {
                Hide();
            }
        }



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }
    }
}
