﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using Model;
using Controller;
using View.Components;
using View.Components.ItemViews;


namespace View.Adapter
{
    public class PacienteViewAdapter : StackPanelAdapter
    {
        public Pagination       Owner   { get; set; }
        public List<Paciente>   Dataset { get; private set; }



        public PacienteViewAdapter( StackPanel container) : base(container) { }



        public void Update(List<Paciente> dataset)
        {
            if (dataset != null)
            {
                Dataset = dataset;
            }
        }



        public override void Build()
        {
            if (Container != null && Dataset != null)
            {
                Container.Children.Clear();

                InternacaoController ctrl = new InternacaoController();

                foreach (Paciente paciente in Dataset)
                {
                    PacienteItemView itemView = new PacienteItemView(this);

                    itemView.icn_sexo.Source = (paciente.Sexo == 0)
                        ? new BitmapImage(new Uri(Path.Resources + "menina.png"))
                        : new BitmapImage(new Uri(Path.Resources + "menino.png"));

                    itemView.icn_cruz.Visibility = (ctrl.PacienteInternado(paciente.ID))
                        ? Visibility.Visible
                        : Visibility.Hidden;

                    itemView.tbk_nome.Text = paciente.Nome;
                    itemView.tbk_responsavel.Text = paciente.Responsavel;
                    itemView.tbk_endereco.Text = paciente.Endereco[0];

                    Container.Children.Add(itemView);
                }
            }
        }
    }
}
