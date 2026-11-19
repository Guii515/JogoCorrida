namespace JogoCorrida
{
    public class Jogo
    {
        public Elemento Carro { get; set; }
        public List<Elemento> Obstaculos { get; set; }
        public int Velocidade { get; set; }
        public int Pontuacao { get; set; }
        public int Tempo { get; set; }
        public int MelhorPontuacao { get; set; }
        public int ColisoesPermitidas { get; set; }
        public int Faixa1Inicio { get; set; }
        public int Faixa1Fim { get; set; }
        public int Faixa2Inicio { get; set; }
        public int Faixa2Fim { get; set; }
        public int Ymaximo { get; set; } = 50;

        // NOVO: informações do placar
        public int Fase { get; set; } = 1;
        public int MoedasColetadas { get; set; }
        public int ObstaculosSuperados { get; set; }
        public int PontosParaFase2 { get; set; } = 300;
        public int Passo { get; set; } = 5; // quantos pixels os obstáculos andam

        // Um único Random para o jogo todo
        private Random rnd = new Random();

        public void IniciaJogo()
        {
            Carro = new Elemento();
            Carro.Tipo = TipoDeElemento.Carro;
            Carro.PosicaoX = PosicionaObjeto(1);
            Carro.PosicaoY = Ymaximo - 100;

            Fase = 1;
            Passo = 5;
            Pontuacao = 0;
            Tempo = 0;
            MoedasColetadas = 0;
            ObstaculosSuperados = 0;

            Obstaculos = FabricaObstaculos(5, 300, 450);
        }

        public List<Elemento> FabricaObstaculos(int qnt, int dMin, int dMax)
        {
            var y_Inicial = 0;
            var obstaculos = new List<Elemento>();

            for (int i = 0; i < qnt; i++)
            {
                if (i != 0)
                    y_Inicial -= rnd.Next(dMin, dMax);
                var ob = new Elemento();
                ob.Tipo = SorteiaTipo();
                var faixa = rnd.Next(1, 3);
                ob.PosicaoX = PosicionaObjeto(faixa);
                ob.PosicaoY = y_Inicial;
                obstaculos.Add(ob);
            }
            return obstaculos;
        }

        // NOVO: sorteia o tipo do elemento. A bomba só aparece na fase 2.
        public TipoDeElemento SorteiaTipo()
        {
            int numero = rnd.Next(1, 101); // 1 a 100

            if (numero <= 25)
                return TipoDeElemento.Moeda;      // 25% de chance
            else if (numero <= 55)
                return TipoDeElemento.Obstaculo;  // 30%
            else if (numero <= 80 || Fase == 1)
                return TipoDeElemento.Galinha;    // 25% (fase 1: o resto também)
            else
                return TipoDeElemento.Bomba;      // 20% (só na fase 2)
        }

        public int PosicionaObjeto(int faixa)
        {
            if (faixa == 1)
                return Faixa1Inicio + ((Faixa1Fim - Faixa1Inicio) / 2);
            else
                return Faixa2Inicio + ((Faixa2Fim - Faixa2Inicio) / 2);
        }

        public void Acelerar(int incremento)
        {
            Velocidade += incremento;
        }

        private int ChecarFaixaElemento(Elemento elemento)
        {
            if (elemento.PosicaoX >= Faixa1Inicio &&
                elemento.PosicaoX <= Faixa1Fim)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        // Verifica se o carro está encostando no elemento
        private bool EstaEncostando(Elemento ob)
        {
            return ChecarFaixaElemento(Carro) == ChecarFaixaElemento(ob) &&
                   Math.Abs(Carro.PosicaoY - ob.PosicaoY) < 60;
        }

        // Batida só conta para obstáculos (moeda não é batida)
        public bool ChecarColisao()
        {
            foreach (var ob in Obstaculos)
            {
                if (ob.Tipo != TipoDeElemento.Moeda && EstaEncostando(ob))
                    return true;
            }
            return false;
        }

        // NOVO: pega a moeda, soma pontos e manda a moeda de volta para cima
        public bool ChecarMoeda()
        {
            foreach (var ob in Obstaculos)
            {
                if (ob.Tipo == TipoDeElemento.Moeda && EstaEncostando(ob))
                {
                    Pontuacao += 50;
                    MoedasColetadas++;
                    Reposiciona(ob);
                    return true;
                }
            }
            return false;
        }

        // NOVO: pontos por tempo (chamar 1 vez por segundo)
        public void ContarSegundo()
        {
            Tempo++;
            Pontuacao += Fase; // fase 1 = +1 por segundo, fase 2 = +2
        }

        // NOVO: troca de fase. Retorna true só no momento em que muda.
        public bool VerificarMudancaFase()
        {
            if (Fase == 1 && Pontuacao >= PontosParaFase2)
            {
                Fase = 2;
                Passo = 8; // obstáculos andam mais rápido
                return true;
            }
            return false;
        }

        public bool VerificarFimJogo()
        {
            if (Pontuacao > MelhorPontuacao)
                MelhorPontuacao = Pontuacao;
            return true;
        }

        public void MovimentaObstaculos()
        {
            foreach (var ob in Obstaculos)
            {
                ob.PosicaoY = ob.PosicaoY + Passo;

                if (ob.PosicaoY > Ymaximo)
                {
                    // Pontos por obstáculo superado (moeda perdida não conta)
                    if (ob.Tipo == TipoDeElemento.Bomba)
                    {
                        Pontuacao += 20;
                        ObstaculosSuperados++;
                    }
                    else if (ob.Tipo != TipoDeElemento.Moeda)
                    {
                        Pontuacao += 10;
                        ObstaculosSuperados++;
                    }

                    Reposiciona(ob);
                }
            }
        }

        // Manda o elemento para cima de todos, com tipo e faixa novos
        private void Reposiciona(Elemento ob)
        {
            int menorY = Obstaculos.Min(x => x.PosicaoY);
            ob.PosicaoY = menorY - rnd.Next(300, 450);
            ob.PosicaoX = PosicionaObjeto(rnd.Next(1, 3));
            ob.Tipo = SorteiaTipo();
        }
    }
}
