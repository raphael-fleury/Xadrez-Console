using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = char.ToUpper(coluna);
            this.linha = linha;
        }

        public PosicaoXadrez(Posicao pos) : 
            this((char)('A' + pos.coluna), 8 - pos.linha) { }

        public Posicao toPosicao()
        {
            return new Posicao(8 - linha, coluna - 'A');
        }

        public override string ToString()
        {
            return coluna + "" + linha;
        }
    }
}
