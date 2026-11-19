using JogoCorrida;
using System.Media;

namespace JogoCorridaWinFormsApp
{
    public partial class FormJogoCorrida : Form
    {
        Jogo jogo;
        string nivel;

        // Controle de tempo
        DateTime tempoUltimaMovimentacao;
        DateTime tempoUltimoSegundo;
        DateTime tempoInicioPartida;
        DateTime tempoAviso;
        DateTime tempoMoeda;

        bool contagemInicial;  // true enquanto mostra 3, 2, 1...
        bool pausado;
        bool mostrarExplosao;
        int xMoeda, yMoeda;    // onde aparece o "+50"

        static int recorde = 0; // melhor pontuação enquanto o programa estiver aberto

        // Imagens carregadas UMA vez e já no tamanho certo (deixa o jogo leve)
        Image imagemCarro = Redimensionar(Properties.Resources.carro_removebg_preview, 80, 80);
        Image imagemCarro2 = Redimensionar(Properties.Resources.carro2_removebg_preview, 80, 80);
        Image imagemGalinha = Redimensionar(Properties.Resources.galinha, 70, 70);
        Image imagemBomba = Redimensionar(Properties.Resources.bomba, 70, 70);
        Image imagemPista = Redimensionar(Properties.Resources.pista1, 400, 550);
        Image imagemMoeda = CriarImagemMoeda();
        Image imagemExplosao = CriarImagemExplosao();

        Font fonteMoeda = new Font("Segoe UI", 16, FontStyle.Bold);
        SoundPlayer somCorrida = new SoundPlayer();

        public FormJogoCorrida(string Nivel)
        {
            // Desenho sem piscar
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            InitializeComponent();
            nivel = Nivel;
            this.FormClosed += FormJogoCorrida_FormClosed;

            IniciarPartida();
        }

        private void IniciarPartida()
        {
            jogo = new Jogo
            {
                Faixa1Inicio = 2,
                Faixa1Fim = 198,
                Faixa2Inicio = 202,
                Faixa2Fim = 398
            };
            jogo.Ymaximo = 550;
            jogo.IniciaJogo();
            jogo.Carro.PosicaoX = jogo.PosicionaObjeto(1);

            // Quanto MENOR, mais rápido (milissegundos entre cada movimento)
            if (nivel == "Fácil")
                jogo.Velocidade = 40;
            else if (nivel == "Médio")
                jogo.Velocidade = 25;
            else
                jogo.Velocidade = 15;

            mostrarExplosao = false;
            pausado = false;
            contagemInicial = true;
            tempoInicioPartida = DateTime.Now;
            tempoMoeda = DateTime.MinValue;

            lblAviso.ForeColor = Color.White;
            lblAviso.Visible = true;
            AtualizarPlacar();

            TocarSomCorrida();
            timerJogo.Enabled = true;
        }

        private void FormJogoCorrida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                jogo.Carro.PosicaoX = jogo.PosicionaObjeto(1);

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                jogo.Carro.PosicaoX = jogo.PosicionaObjeto(2);

            if (e.KeyCode == Keys.P && !contagemInicial)
            {
                pausado = !pausado;
                lblAviso.ForeColor = Color.White;
                lblAviso.Text = "PAUSADO";
                lblAviso.Visible = pausado;
            }

            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void timerJogo_Tick(object sender, EventArgs e)
        {
            if (pausado)
                return;

            // Contagem 3, 2, 1, VAI! antes de começar
            if (contagemInicial)
            {
                double segundos = (DateTime.Now - tempoInicioPartida).TotalSeconds;
                if (segundos < 3)
                {
                    lblAviso.Text = (3 - (int)segundos).ToString();
                }
                else
                {
                    contagemInicial = false;
                    lblAviso.Text = "VAI!";
                    tempoAviso = DateTime.Now;
                    tempoUltimaMovimentacao = DateTime.Now;
                    tempoUltimoSegundo = DateTime.Now;
                }
                Invalidate();
                return;
            }

            // Movimento dos obstáculos e da pista
            if ((DateTime.Now - tempoUltimaMovimentacao).TotalMilliseconds >= jogo.Velocidade)
            {
                tempoUltimaMovimentacao = DateTime.Now;
                jogo.MovimentaObstaculos();
            }

            // 1a) Pontos por tempo: a cada 1 segundo
            if ((DateTime.Now - tempoUltimoSegundo).TotalMilliseconds >= 1000)
            {
                tempoUltimoSegundo = DateTime.Now;
                jogo.ContarSegundo();
            }

            // 1c) Moedas: mostra "+50" onde pegou
            if (jogo.ChecarMoeda())
            {
                tempoMoeda = DateTime.Now;
                xMoeda = jogo.Carro.PosicaoX - 25;
                yMoeda = jogo.Carro.PosicaoY - 30;
            }

            // 4) Troca de fase
            if (jogo.VerificarMudancaFase())
            {
                lblAviso.ForeColor = Color.Orange;
                lblAviso.Text = "FASE 2!";
                lblAviso.Visible = true;
                tempoAviso = DateTime.Now;
            }

            // Esconde o aviso depois de 2 segundos
            if (lblAviso.Visible && (DateTime.Now - tempoAviso).TotalMilliseconds >= 2000)
                lblAviso.Visible = false;

            AtualizarPlacar();
            Invalidate(); // manda redesenhar a tela (chama o Paint)

            if (jogo.ChecarColisao())
                GameOver();
        }

        // Tudo do jogo é desenhado aqui: pista, obstáculos e carro
        private void FormJogoCorrida_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            DesenharPista(g);

            // Obstáculos e moedas (centralizados na faixa)
            foreach (var ob in jogo.Obstaculos)
            {
                if (ob.PosicaoY > -100)
                {
                    var imagem = ImagemDoTipo(ob.Tipo);
                    g.DrawImage(imagem, ob.PosicaoX - imagem.Width / 2, ob.PosicaoY, imagem.Width, imagem.Height);
                }
            }

            // Carro do jogador (ou a explosão)
            int xCarro = jogo.Carro.PosicaoX - 40;
            if (mostrarExplosao)
            {
                g.DrawImage(imagemExplosao, xCarro, jogo.Carro.PosicaoY, 80, 80);
                // tela avermelhada no momento da batida
                using (var vermelho = new SolidBrush(Color.FromArgb(90, 255, 0, 0)))
                    g.FillRectangle(vermelho, ClientRectangle);
            }
            else
            {
                g.DrawImage(imagemCarro, xCarro, jogo.Carro.PosicaoY, 80, 80);
            }

            // "+50" subindo por meio segundo depois de pegar moeda
            double msMoeda = (DateTime.Now - tempoMoeda).TotalMilliseconds;
            if (msMoeda < 600)
                g.DrawString("+50", fonteMoeda, Brushes.Gold, xMoeda, yMoeda - (int)(msMoeda / 15));
        }

        // Pista original do jogo (a mesma imagem de antes).
        // Na fase 2 só escurece um pouco para mostrar que mudou de fase.
        private void DesenharPista(Graphics g)
        {
            g.DrawImage(imagemPista, 0, 0, ClientSize.Width, ClientSize.Height);

            if (jogo.Fase == 2)
            {
                using (var escuro = new SolidBrush(Color.FromArgb(70, 0, 0, 40)))
                    g.FillRectangle(escuro, ClientRectangle);
            }
        }

        private void AtualizarPlacar()
        {
            lblPontos.Text = "Pontos: " + jogo.Pontuacao;
            lblTempo.Text = "Tempo: " + jogo.Tempo + "s";
            lblMoedas.Text = "Moedas: " + jogo.MoedasColetadas;
            lblFase.Text = "FASE " + jogo.Fase;
            lblRecorde.Text = "Recorde: " + recorde;
        }

        // 3) Cada tipo de obstáculo tem uma imagem
        private Image ImagemDoTipo(TipoDeElemento tipo)
        {
            if (tipo == TipoDeElemento.Galinha)
                return imagemGalinha;
            else if (tipo == TipoDeElemento.Bomba)
                return imagemBomba;
            else if (tipo == TipoDeElemento.Moeda)
                return imagemMoeda;
            else
                return imagemCarro2;
        }

        private void GameOver()
        {
            timerJogo.Enabled = false;
            TocarSomBatida();
            EfeitoBatida();

            bool novoRecorde = jogo.Pontuacao > recorde;
            if (novoRecorde)
                recorde = jogo.Pontuacao;
            AtualizarPlacar();

            string mensagem =
                (novoRecorde ? "NOVO RECORDE!\n\n" : "BATEU!\n\n") +
                "Pontos: " + jogo.Pontuacao + "\n" +
                "Tempo: " + jogo.Tempo + "s\n" +
                "Obstáculos superados: " + jogo.ObstaculosSuperados + "\n" +
                "Moedas: " + jogo.MoedasColetadas + "\n" +
                "Fase alcançada: " + jogo.Fase + "\n\n" +
                "Jogar de novo?";

            var resposta = MessageBox.Show(mensagem, "Game Over", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.Yes)
                IniciarPartida();
            else
                Close();
        }

        // 2) Efeito visual da batida: explosão piscando + tela vermelha + tela tremendo
        private void EfeitoBatida()
        {
            var posicaoOriginal = this.Location;

            for (int i = 0; i < 12; i++)
            {
                mostrarExplosao = (i % 2 == 0);

                if (i % 2 == 0)
                    this.Location = new Point(posicaoOriginal.X + 10, posicaoOriginal.Y);
                else
                    this.Location = new Point(posicaoOriginal.X - 10, posicaoOriginal.Y);

                this.Refresh();
                Thread.Sleep(70);
            }

            this.Location = posicaoOriginal;
            mostrarExplosao = true; // termina explodido
            this.Refresh();
        }

        private void FormJogoCorrida_FormClosed(object? sender, FormClosedEventArgs e)
        {
            timerJogo.Enabled = false;
            somCorrida.Stop();
        }

        // Cria uma cópia da imagem no tamanho desejado
        private static Image Redimensionar(Image imagem, int largura, int altura)
        {
            return new Bitmap(imagem, largura, altura);
        }

        // Desenha uma moeda dourada
        private static Image CriarImagemMoeda()
        {
            var bmp = new Bitmap(60, 60);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.DarkGoldenrod, 5, 5, 50, 50);
                g.FillEllipse(Brushes.Gold, 10, 10, 40, 40);
                g.DrawString("$", new Font("Arial", 20, FontStyle.Bold), Brushes.DarkGoldenrod, 17, 13);
            }
            return bmp;
        }

        // Desenha uma explosão (círculos vermelho, laranja e amarelo)
        private static Image CriarImagemExplosao()
        {
            var bmp = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.Red, 0, 0, 80, 80);
                g.FillEllipse(Brushes.Orange, 12, 12, 56, 56);
                g.FillEllipse(Brushes.Yellow, 25, 25, 30, 30);
            }
            return bmp;
        }

        private void TocarSomCorrida()
        {
            var caminho = Path.Combine(Application.StartupPath, "Sons", "corrida.wav");
            if (File.Exists(caminho))
            {
                somCorrida.SoundLocation = caminho;
                somCorrida.PlayLooping();
            }
        }

        private void TocarSomBatida()
        {
            somCorrida.Stop();
            var caminho = Path.Combine(Application.StartupPath, "Sons", "batida.wav");
            if (File.Exists(caminho))
            {
                var sp = new SoundPlayer(caminho);
                sp.Play();
            }
        }
    }
}
