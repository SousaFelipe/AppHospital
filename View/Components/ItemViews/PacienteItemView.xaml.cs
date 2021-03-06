﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;

using Model;
using Controller;
using View.Adapter;
using View.Components.Dialogs;


namespace View.Components.ItemViews
{
    public partial class PacienteItemView : UserControl
    {
        private PacienteViewAdapter Owner { get; set; }



        public Paciente Item
        {
            get
            {
                return new PacienteController().Buscar(
                    new string[] { "nome", "responsavel" }, new string[] { tbk_nome.Text, tbk_responsavel.Text }
                );
            }
        }



        public PacienteItemView(PacienteViewAdapter owner)
        {
            InitializeComponent();
            Owner = owner;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MouseObserver);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }



        private void MouseObserver(object sender, EventArgs e)
        {
            double x = Mouse.GetPosition(this).X;
            double y = Mouse.GetPosition(this).Y;

            if ((x > 0 && x < ActualWidth) && (y > 0 && y < ActualHeight))
            {
                Background = new SolidColorBrush(Color.FromRgb(224, 247, 250));
                grd_controles.Visibility = Visibility.Visible;
            }
            else
            {
                Background = new SolidColorBrush(Colors.Transparent);
                grd_controles.Visibility = Visibility.Hidden;
            }
        }



        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PacienteDialog dialog = new PacienteDialog(Owner.Owner.Owner);
            dialog.CarregarPaciente(Item.ID);
            dialog.ExibirInformacoes();
            dialog.Show();
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            
            if (item.Name.Equals("history"))
            {
                new HistoricoDialog(Owner.Owner.Owner).Show(Item);
            }
            else if (item.Name.Equals("edit"))
            {
                new NovoPacienteDialog(Owner.Owner.Owner).Show(Item);
            }
            else if (item.Name.Equals("delete"))
            {
                Delete();
            }
        }



        private void Delete()
        {
            MessageBoxResult result = MessageBox.Show(
                "Você realmente deseja remover este Paciente?\nEsta ação é ireversível!",
                "Removendo paciente...",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No
            );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(new PacienteController().Remover(Item.ID), "Removendo paciente...", MessageBoxButton.OK, MessageBoxImage.Information);
                Owner.Owner.Owner.Refresh(string.Empty);
            }
        }
    }
}
