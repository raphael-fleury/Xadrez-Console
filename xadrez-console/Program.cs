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

                Console.Write("Digite a posição de origem: ");
                Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                Console.Write("Digite a posição de destino: ");
                Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                partida.executarMovimento(origem, destino);
            }

            Console.ReadKey();
        }
    }
}
