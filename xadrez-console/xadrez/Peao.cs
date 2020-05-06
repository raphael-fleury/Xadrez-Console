using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && cor != p.cor;
        }

        public bool enPassantEsquerdaDisponivel()
        {
            if (!(cor == Cor.BRANCO && posicao.linha == 3) && !(cor == Cor.PRETO && posicao.linha == 4))
                return false;

            Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
            return tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant;
        }

        public bool enPassantDireitaDisponivel()
        {
            if (!(cor == Cor.BRANCO && posicao.linha == 3) && !(cor == Cor.PRETO && posicao.linha == 4))
                return false;

            Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
            return tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];
            Posicao pos;

            int i = cor == Cor.BRANCO ? -1 : 1;
            
            pos = new Posicao(posicao.linha + i, posicao.coluna);
            if (tab.posicaoValida(pos) && !tab.existePeca(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + i * 2, posicao.coluna);
            if (tab.posicaoValida(pos) && !tab.existePeca(pos) && qtdMovimentos < 1)
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + i, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
                matriz[pos.linha, pos.coluna] = true;

            pos = new Posicao(posicao.linha + i, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
                matriz[pos.linha, pos.coluna] = true;

            // #jogadaespecial en passant

            //esquerda
            pos = new Posicao(posicao.linha + i, posicao.coluna - 1);
            if (tab.posicaoValida(pos))
                matriz[pos.linha, pos.coluna] = enPassantEsquerdaDisponivel();

            //direita
            pos = new Posicao(posicao.linha + i, posicao.coluna + 1);
            if (tab.posicaoValida(pos))
                matriz[pos.linha, pos.coluna] = enPassantDireitaDisponivel();

            return matriz;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
