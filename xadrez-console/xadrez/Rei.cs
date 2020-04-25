using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            //acima
            pos = new Posicao(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //abaixo
            pos = new Posicao(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //esquerda
            pos = new Posicao(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //direita
            pos = new Posicao(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //noroeste
            pos = new Posicao(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //sudoeste
            pos = new Posicao(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //nordeste
            pos = new Posicao(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            //sudeste
            pos = new Posicao(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[pos.linha, pos.coluna] = true;

            return matriz;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
