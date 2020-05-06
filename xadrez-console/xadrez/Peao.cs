using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && cor != p.cor;
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

            return matriz;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
