namespace JogoCorridaWinFormsApp
{
    partial class FormJogoCorrida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerJogo = new System.Windows.Forms.Timer(components);
            lblPontos = new Label();
            lblTempo = new Label();
            lblMoedas = new Label();
            lblFase = new Label();
            lblRecorde = new Label();
            lblAviso = new Label();
            SuspendLayout();
            //
            // timerJogo
            //
            timerJogo.Interval = 15;
            timerJogo.Tick += timerJogo_Tick;
            //
            // lblPontos
            //
            lblPontos.AutoSize = true;
            lblPontos.BackColor = Color.Black;
            lblPontos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPontos.ForeColor = Color.Yellow;
            lblPontos.Location = new Point(12, 8);
            lblPontos.Name = "lblPontos";
            lblPontos.Text = "Pontos: 0";
            //
            // lblTempo
            //
            lblTempo.AutoSize = true;
            lblTempo.BackColor = Color.Black;
            lblTempo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTempo.ForeColor = Color.White;
            lblTempo.Location = new Point(12, 34);
            lblTempo.Name = "lblTempo";
            lblTempo.Text = "Tempo: 0s";
            //
            // lblMoedas
            //
            lblMoedas.AutoSize = true;
            lblMoedas.BackColor = Color.Black;
            lblMoedas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMoedas.ForeColor = Color.Gold;
            lblMoedas.Location = new Point(12, 56);
            lblMoedas.Name = "lblMoedas";
            lblMoedas.Text = "Moedas: 0";
            //
            // lblFase
            //
            lblFase.AutoSize = true;
            lblFase.BackColor = Color.Black;
            lblFase.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFase.ForeColor = Color.Lime;
            lblFase.Location = new Point(310, 8);
            lblFase.Name = "lblFase";
            lblFase.Text = "FASE 1";
            //
            // lblRecorde
            //
            lblRecorde.AutoSize = true;
            lblRecorde.BackColor = Color.Black;
            lblRecorde.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRecorde.ForeColor = Color.Orange;
            lblRecorde.Location = new Point(300, 34);
            lblRecorde.Name = "lblRecorde";
            lblRecorde.Text = "Recorde: 0";
            //
            // lblAviso
            //
            lblAviso.BackColor = Color.Transparent;
            lblAviso.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblAviso.ForeColor = Color.Orange;
            lblAviso.Location = new Point(0, 180);
            lblAviso.Name = "lblAviso";
            lblAviso.Size = new Size(400, 120);
            lblAviso.Text = "";
            lblAviso.TextAlign = ContentAlignment.MiddleCenter;
            lblAviso.Visible = false;
            //
            // FormJogoCorrida
            //
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(400, 550);
            Controls.Add(lblAviso);
            Controls.Add(lblPontos);
            Controls.Add(lblTempo);
            Controls.Add(lblMoedas);
            Controls.Add(lblFase);
            Controls.Add(lblRecorde);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "FormJogoCorrida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Jogo Corrida - IFSP";
            Paint += FormJogoCorrida_Paint;
            KeyDown += FormJogoCorrida_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timerJogo;
        private Label lblPontos;
        private Label lblTempo;
        private Label lblMoedas;
        private Label lblFase;
        private Label lblRecorde;
        private Label lblAviso;
    }
}
