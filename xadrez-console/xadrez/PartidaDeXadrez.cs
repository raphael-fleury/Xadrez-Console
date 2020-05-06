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
        public Peca vulneravelEnPassant { get; private set; }

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

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca r = rei(cor);
            if (r == null)
                throw new TabuleiroException("Não há um rei " + cor.ToString().ToLower() + "!");

            foreach (Peca p in pecasEmJogo(corAdversaria(cor)))
            {
                bool[,] matriz = p.movimentosPossiveis();
                if (matriz[r.posicao.linha, r.posicao.coluna])
                    return true;
            }

            return false;
        }

        public bool estaEmXequemate(Cor cor)
        {
            foreach(Peca p in pecasEmJogo(cor))
            {
                bool[,] matriz = p.movimentosPossiveis();

                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (matriz[i, j])
                        {
                            Posicao origem = p.posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool xeque = estaEmXeque(cor);
                            desfazerMovimento(origem, destino, pecaCapturada);

                            if (!xeque)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            xeque = estaEmXeque(corAdversaria(jogadorAtual));

            if (estaEmXequemate(corAdversaria(jogadorAtual)) || pecaCapturada is Rei)
                terminada = true;
            else
            {
                turno++;
                jogadorAtual = corAdversaria(jogadorAtual);
            }

            Peca p = tab.peca(destino);

            // #jogadaespecial en passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
                vulneravelEnPassant = p;
            else
                vulneravelEnPassant = null;
        }

        public void desfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            tab.colocarPeca(p, origem);
            p.decrementarMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            // #jogadaEspecial

            // roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            // roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            // en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    tab.colocarPeca(peao, 
                        new Posicao(p.cor == Cor.BRANCO ? 3 : 4, destino.coluna)
                    );
                }
            }
        }

        private Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
          
            // #jogadaEspecial
            // roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                executarMovimento(origemTorre, destinoTorre);
            }

            // roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna  - 1);
                executarMovimento(origemTorre, destinoTorre);
            }

            //en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posPeao = new Posicao(destino.linha + (p.cor == Cor.BRANCO ? 1 : -1),
                        destino.coluna
                    );
                    pecaCapturada = tab.retirarPeca(posPeao);
                }
            }

            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.BRANCO));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.BRANCO));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.BRANCO));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.BRANCO));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.BRANCO, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.BRANCO));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.BRANCO));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.BRANCO));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.BRANCO, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.BRANCO, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.PRETO));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.PRETO));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.PRETO));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.PRETO));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.PRETO, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.PRETO));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.PRETO));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.PRETO));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.PRETO, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.PRETO, this));
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
