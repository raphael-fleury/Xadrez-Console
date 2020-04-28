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
        public bool xeque { get; private set; }

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

        public bool reiEstaEmXeque(Cor cor)
        {
            Peca r = rei(cor);
            if (r == null)
                throw new TabuleiroException("Não há um rei da cor " + cor.ToString().ToLower() + "!");

            foreach (Peca p in pecasEmJogo(corAdversaria(cor)))
            {
                bool[,] matriz = p.movimentosPossiveis();
                if (matriz[r.posicao.linha, r.posicao.coluna])
                    return true;
            }

            return false;
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);

            if (reiEstaEmXeque(jogadorAtual))
            {
                desfazerJogada(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            xeque = reiEstaEmXeque(corAdversaria(jogadorAtual));

            turno++;
            jogadorAtual = corAdversaria(jogadorAtual);
        }

        public void desfazerJogada(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            tab.colocarPeca(p, origem);
            p.decrementarMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
        }

        private Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            return pecaCapturada;
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

        private Cor corAdversaria(Cor cor)
        {
            if (cor == Cor.BRANCO)
                return Cor.PRETO;
            else
                return Cor.BRANCO;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca p in pecasEmJogo(cor))
            {
                if (p is Rei)
                    return p;
            }

            return null;
        }
    }
}
