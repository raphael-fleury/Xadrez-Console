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
                Console.Clear();
                Tela.ImprimirTabuleiro(partida.tab);
                Console.WriteLine();

                try
                {
                    Console.Write("Digite a posição de origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    Peca peca = partida.tab.peca(origem);
                    bool[,] posicoesPossiveis = peca.movimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine($"\nPeça selecionada: {peca} ({new PosicaoXadrez(origem)})");
                    Console.Write("Digite a posição de destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executarMovimento(origem, destino);
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);
                    Console.Write("Erro. " + e.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
