using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFácil_Click(object sender, EventArgs e) {
            var jogo = new FormJogoCorrida("Fácil");
            jogo.ShowDialog();
        }

        private void btnMedio_Click(object sender, EventArgs e)
        {
            var jogo = new FormJogoCorrida("Médio");
            jogo.ShowDialog(); ;
        }

        private void btnDificil_Click(object sender, EventArgs e)
        {
            var jogo = new FormJogoCorrida("Difícil");
            jogo.ShowDialog(); ;
        }
    }
}
