using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos;

            pos = new Posicao(posicao.linha - 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha - 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha - 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha - 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            return matriz;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
