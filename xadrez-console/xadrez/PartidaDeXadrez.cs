using System.Collections.Generic;
using System.Linq;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public bool terminada { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.BRANCO;

            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            return capturadas.Where(p => p.cor == cor).ToHashSet();
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            return pecas.Where(p => p.cor == cor).Except(pecasCapturadas(cor)).ToHashSet();
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            jogadorAtual = (Cor)(-(int)jogadorAtual);
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            tab.validarPosicao(pos);

            if (tab.peca(pos) == null)
                throw new TabuleiroException("Não há uma peça nessa posição!");
            if (jogadorAtual != tab.peca(pos).cor)
                throw new TabuleiroException("A peça escolhida não é sua!");
            if (!tab.peca(pos).existeMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida!");
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            tab.validarPosicao(destino);
            if (!tab.peca(origem).podeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        public void colocarNovaPeca(Peca peca, char coluna, int linha)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.movimentar();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);
        }

        private void colocarPecas()
        {
            colocarNovaPeca(new Torre(tab, Cor.BRANCO), 'c', 1);
            colocarNovaPeca(new Torre(tab, Cor.BRANCO), 'c', 2);
            colocarNovaPeca(new Rei(tab, Cor.BRANCO), 'd', 1);
            colocarNovaPeca(new Torre(tab, Cor.BRANCO), 'd', 2);
            colocarNovaPeca(new Torre(tab, Cor.BRANCO), 'e', 1);
            colocarNovaPeca(new Torre(tab, Cor.BRANCO), 'e', 2);

            colocarNovaPeca(new Torre(tab, Cor.PRETO), 'c', 7);
            colocarNovaPeca(new Torre(tab, Cor.PRETO), 'c', 8);            
            colocarNovaPeca(new Torre(tab, Cor.PRETO), 'd', 7);
            colocarNovaPeca(new Rei(tab, Cor.PRETO), 'd', 8);
            colocarNovaPeca(new Torre(tab, Cor.PRETO), 'e', 7);
            colocarNovaPeca(new Torre(tab, Cor.PRETO), 'e', 8);
        }
    }
}
