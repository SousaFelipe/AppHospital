using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;


namespace View.Components
{
    public partial class NovoPacienteDialog : UserControl
    {
        private DispatcherTimer Timer { get; set; }
        private Window          Owner { get; set; }



        private Grid ContentOwner
        {
            get
            {
                return (Owner != null) ? (Grid)Owner.Content : null;
            }
        }



        public NovoPacienteDialog(Window owner)
        {
            InitializeComponent();

            Owner = owner;
        }



        public void Show()
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



        private bool CamposVazios()
        {
            if (string.IsNullOrEmpty(txb_nome_paciente.Text))
                return true;

            if (string.IsNullOrEmpty(dpk_data_nascimento.Text))
                return true;

            if (string.IsNullOrEmpty(txb_cartao_sus.Text))
                return true;

            if (cbx_sexo.SelectedIndex <= 0)
                return true;

            if (string.IsNullOrEmpty(txb_nome_responsavel.Text))
                return true;

            if (string.IsNullOrEmpty(txb_telefone_responsavel.Text))
                return true;

            if (string.IsNullOrEmpty(txb_bairro_distrito.Text))
                return true;

            if (string.IsNullOrEmpty(txb_rua.Text))
                return true;

            return false;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = ((Button)sender);

            if (button.Content.Equals("SALVAR"))
            {
                if (CamposVazios())
                {
                    MessageBox.Show("Um ou mais campos importantes estão vazios!", "Pacientes", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Paciente paciente = new Paciente();
                    paciente.Nome = txb_nome_paciente.Text;
                    paciente.DataNascimento = Convert.ToDateTime(dpk_data_nascimento.Text);
                    paciente.CartaoSus = txb_cartao_sus.Text;
                    paciente.Sexo = (cbx_sexo.SelectedIndex - 1);
                    paciente.Responsavel = txb_nome_responsavel.Text;
                    paciente.Telefone = PacienteController.Formatar(txb_telefone_responsavel.Text);
                    paciente.Endereco = new string[] { txb_bairro_distrito.Text, txb_rua.Text, txb_numero_casa.Text };

                    Hide();

                    MessageBox.Show(
                        new PacienteController().Inserir(paciente), "Salvando paciente...", MessageBoxButton.OK, MessageBoxImage.Information
                    );

                    Owner.Refresh();
                }
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
