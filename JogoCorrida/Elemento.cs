namespace JogoCorrida
{
    public class Elemento
    {
        public TipoDeElemento Tipo { get; set; }
        public int PosicaoX;
        public int PosicaoY;
        public int Altura { get; set; }
        public int Largura { get; set; }

        public void Movimentar(int x, int y) { 
        
           PosicaoX = x;
           PosicaoY = y;

        }

    }
}
