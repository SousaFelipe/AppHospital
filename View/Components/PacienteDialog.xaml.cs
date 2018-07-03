using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

using Model;
using Controller;

namespace View.Components
{
    public partial class PacienteDialog : UserControl
    {
        private DispatcherTimer Timer { get; set; }
        private Window          Owner { get; set; }


        
        private Grid ContentOwner
        {
            get
            {
                return (Owner != null) ? (Grid) Owner.Content : null;
            }
        }



        public PacienteDialog(Window owner)
        {
            InitializeComponent();

            Owner = owner;
        }



        public void CarregarPaciente(int id)
        {
            Paciente paciente = new PacienteController().Buscar(new string[] { "id" }, new string[] { id.ToString() });

            if (paciente == null)
            {
                MessageBox.Show("Paciente desconhecido!", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                icn_sexo.Source = (paciente.Sexo == 0)
                ? new BitmapImage(new Uri(Path.Resources + "menina.png"))
                : new BitmapImage(new Uri(Path.Resources + "menino.png"));

                tbk_nome.Text = paciente.Nome;
                tbk_data_nascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
                tbk_cartao_sus.Text = string.Format("{0:### #### #### ####}", Convert.ToInt64(paciente.CartaoSus));
                tbk_responsavel.Text = paciente.Responsavel;
                tbk_telefone.Text = string.Format("{0:(##) # ####-####}", Convert.ToInt64(paciente.Telefone));
                tbk_bairro.Text = paciente.Endereco[0];
                tbk_rua.Text = paciente.Endereco[1];

                if (string.IsNullOrEmpty(paciente.Endereco[2]))
                {
                    tbk_sub_rua.Text = "Rua/Localidade";
                }
                else
                {
                    tbk_sub_rua.Text = "Rua/Localidade - N°";
                    tbk_rua.Text += " - " + paciente.Endereco[2];
                }
            }
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



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
