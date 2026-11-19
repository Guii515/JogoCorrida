namespace JogoCorridaWinFormsApp
{
    partial class FormInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnSair = new Button();
            btnDificil = new Button();
            btnMedio = new Button();
            btnFácil = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSair);
            groupBox1.Controls.Add(btnDificil);
            groupBox1.Controls.Add(btnMedio);
            groupBox1.Controls.Add(btnFácil);
            groupBox1.Font = new Font("Segoe UI", 20F);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 206);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "SELECIONE O NÍVEL";
            // 
            // btnSair
            // 
            btnSair.Location = new Point(195, 144);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(96, 56);
            btnSair.TabIndex = 3;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // btnDificil
            // 
            btnDificil.BackColor = Color.Red;
            btnDificil.Font = new Font("Segoe UI", 30F);
            btnDificil.Location = new Point(324, 79);
            btnDificil.Name = "btnDificil";
            btnDificil.Size = new Size(155, 59);
            btnDificil.TabIndex = 2;
            btnDificil.Text = "Difícil";
            btnDificil.UseVisualStyleBackColor = false;
            btnDificil.Click += btnDificil_Click;
            // 
            // btnMedio
            // 
            btnMedio.BackColor = Color.FromArgb(255, 128, 0);
            btnMedio.Font = new Font("Segoe UI", 30F);
            btnMedio.Location = new Point(163, 79);
            btnMedio.Name = "btnMedio";
            btnMedio.Size = new Size(155, 59);
            btnMedio.TabIndex = 1;
            btnMedio.Text = "Médio";
            btnMedio.UseVisualStyleBackColor = false;
            btnMedio.Click += btnMedio_Click;
            // 
            // btnFácil
            // 
            btnFácil.BackColor = Color.FromArgb(192, 255, 192);
            btnFácil.Font = new Font("Segoe UI", 30F);
            btnFácil.Location = new Point(15, 79);
            btnFácil.Name = "btnFácil";
            btnFácil.Size = new Size(142, 59);
            btnFácil.TabIndex = 0;
            btnFácil.Text = "Fácil";
            btnFácil.UseVisualStyleBackColor = false;
            btnFácil.Click += btnFácil_Click;
            // 
            // FormInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 230);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormInicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormInicio";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnDificil;
        private Button btnMedio;
        private Button btnFácil;
        private Button btnSair;
    }
}