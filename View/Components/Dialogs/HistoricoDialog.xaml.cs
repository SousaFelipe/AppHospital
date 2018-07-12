﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;
using View.Adapter;


namespace View.Components.Dialogs
{
    public partial class HistoricoDialog : UserControl
    {
        private Window          Owner { get; set; }
        private DispatcherTimer Timer { get; set; }
        private Paciente        Atual { get; set; }




        private Grid ContentOwner
        {
            get
            {
                return (Owner != null) ? ((Grid)Owner.Content) : null;
            }
        }



        public HistoricoDialog(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }



        public void Show(Paciente paciente)
        {
            Atual = paciente;

            tbk_paciente.Text = paciente.Nome;

            InternacaoViewAdapter adapter = new InternacaoViewAdapter(stp_internacoes);
            adapter.Dataset = new InternacaoController().Listar(Atual.ID);
            adapter.Build();

            tbk_nenhuma_in.Visibility = (adapter.Container.Children.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            grd_internacoes.Visibility = (adapter.Container.Children.Count > 0) ? Visibility.Visible : Visibility.Hidden;

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