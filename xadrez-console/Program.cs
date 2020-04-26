using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();

            while (!partida.terminada)
            {
                try
                {
                    Tela.ImprimirPartida(partida);

                    Console.Write("Digite a posição de origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    partida.validarPosicaoDeOrigem(origem);

                    Peca peca = partida.tab.peca(origem);
                    bool[,] posicoesPossiveis = peca.movimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine($"\nPeça selecionada: {peca} ({new PosicaoXadrez(origem)})");
                    Console.Write("Digite a posição de destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.validarPosicaoDeDestino(origem, destino);

                    partida.realizarJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);                    
                    Tela.ImprimirErro(e);
                }
                catch (Exception)
                {
                    Console.Clear();                   
                    Tela.ImprimirTabuleiro(partida.tab);
                    Tela.ImprimirErro("Erro.");
                }
            }

            Console.ReadKey();
        }
    }
}
