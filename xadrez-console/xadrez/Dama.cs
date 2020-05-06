using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos;

            //acima
            pos = new Posicao(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha--;
            }

            //abaixo
            pos = new Posicao(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha++;
            }

            //esquerda
            pos = new Posicao(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.coluna--;
            }

            //direita
            pos = new Posicao(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.coluna++;
            }

            //noroeste
            pos = new Posicao(posicao.linha - 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha--;
                pos.coluna--;
            }

            //nordeste
            pos = new Posicao(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha--;
                pos.coluna++;
            }

            //sudoeste
            pos = new Posicao(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha++;
                pos.coluna--;
            }

            //sudeste
            pos = new Posicao(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.linha, pos.coluna] = true;
                if (tab.peca(pos).cor != cor)
                    break;

                pos.linha++;
                pos.coluna++;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
