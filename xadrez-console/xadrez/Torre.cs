using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos;

            //acima
            pos = new Posicao(posicao.linha - 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null)
                    break;

                pos.linha--;
            }

            //abaixo
            pos = new Posicao(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null)
                    break;

                pos.linha++;
            }

            //esquerda
            pos = new Posicao(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null)
                    break;

                pos.coluna--;
            }

            //direita
            pos = new Posicao(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null)
                    break;

                pos.coluna++;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
