using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;


namespace View.Components.Dialogs
{
    public partial class AltaDialog : UserControl
    {
        private Window          Owner { get; set; }
        private DispatcherTimer Timer { get; set; }
        private Internacao      Atual { get; set; }



        private Grid ContentOwner
        {
            get
            {
                return (Owner != null) ? ((Grid)Owner.Content) : null;
            }
        }



        public AltaDialog(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }



        public void Show(Internacao internacao)
        {
            Atual = internacao;

            tbk_paciente.Text = new PacienteController().Buscar(new string[] { "id" }, new string[] { internacao.Paciente.ToString() }).Nome;
            tbk_causa.Text = Atual.Causa[0].ToString().ToUpper() + Atual.Causa.Substring(1);
            tbk_data_entrada.Text = Atual.DataEntrada.ToString("dd/MM/yyyy");
            dpk_data_saida.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txb_nota.Text = (string.IsNullOrEmpty(Atual.Nota))
                ? string.Empty
                : Atual.Nota[0].ToString().ToUpper() + Atual.Nota.Substring(1);

            AddToContent();
        }



        public void AddToContent()
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
            if (string.IsNullOrEmpty(dpk_data_saida.Text))
                return true;

            return false;
        }



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content.Equals("SALVAR"))
            {
                if (CamposVazios())
                {
                    MessageBox.Show(
                        "Algumas informações importantes não foram adicionadas!", "Ops!", MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                }
                else
                {
                    Atual.DataSaida = Convert.ToDateTime(dpk_data_saida.Text);
                    Atual.Nota = txb_nota.Text;

                    MessageBox.Show(
                        new InternacaoController().Inserir(Atual), "Salvando internação...", MessageBoxButton.OK, MessageBoxImage.Information
                    );

                    Hide();
                    Owner.Refresh(string.Empty);
                }
            }
            else if (button.Content.Equals("CANCELAR"))
            {
                Hide();
            }
        }
    }
}