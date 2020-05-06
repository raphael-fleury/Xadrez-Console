using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        private bool podeFazerRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.qtdMovimentos < 1 && cor == p.cor;
        }

        public bool roquePequenoDisponivel()
        {
            if (qtdMovimentos > 0 || partida.xeque)
                return false;

            Posicao T1 = new Posicao(posicao.linha, posicao.coluna + 3);
            if (podeFazerRoque(T1))
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);

                if (tab.peca(p1) == null && tab.peca(p2) == null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool roqueGrandeDisponivel()
        {
            if (qtdMovimentos > 0 || partida.xeque)
                return false;

            Posicao posTorre = new Posicao(posicao.linha, posicao.coluna - 4);
            if (podeFazerRoque(posTorre))
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    return true;
            }

            return false;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos;

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

            // #jogadaEspecial roque pequeno
            pos = new Posicao(posicao.linha, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[posicao.linha, posicao.coluna + 2] = roquePequenoDisponivel();

            // #jogadaEspecial roque grande
            pos = new Posicao(posicao.linha, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
                matriz[posicao.linha, posicao.coluna - 2] = roqueGrandeDisponivel();
            
            return matriz;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
