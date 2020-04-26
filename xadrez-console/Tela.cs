using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            return new PosicaoXadrez(s[0], int.Parse(s[1] + ""));
        }

        public static void Imprimir(object obj, ConsoleColor cor)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = cor;
            Console.Write(obj);
            Console.ForegroundColor = defaultColor;
        }

        public static void ImprimirLinha(object obj, ConsoleColor cor)
        {
            Imprimir(obj + "\n", cor);
        }

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno " + partida.turno);
            Console.WriteLine("Aguardando jogador " + partida.jogadorAtual + "\n");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int y = 0; y < tab.linhas; y++)
            {
                Console.Write(tab.linhas - y + " ");
                for (int x = 0; x < tab.colunas; x++)
                {
                    ImprimirPeca(tab.peca(y, x));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int y = 0; y < tab.linhas; y++)
            {
                Console.Write(tab.linhas - y + " ");
                for (int x = 0; x < tab.colunas; x++)
                {
                    if (posicoesPossiveis[y, x])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;

                    ImprimirPeca(tab.peca(y, x));
                    Console.Write(" ");
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            ImprimirLinha("Peças capturadas", ConsoleColor.Yellow);
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.BRANCO));
            Console.WriteLine();
            Console.Write("Pretas:  ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.PRETO));
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Imprimir(" " + peca, peca.cor == Cor.BRANCO ? ConsoleColor.White : ConsoleColor.Yellow);
            }
            Console.Write(" ]");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
                Console.Write("-");
            else
            {
                ConsoleColor defaultColor = Console.ForegroundColor;
                switch (peca.cor)
                {
                    case Cor.BRANCO:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case Cor.PRETO:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        break;
                }
                Console.Write(peca);
                Console.ForegroundColor = defaultColor;
            }
        }

        public static void ImprimirErro(string erro)
        {
            Console.WriteLine();
            ImprimirLinha(erro, ConsoleColor.Red);
            Console.WriteLine("Aperte alguma tecla para continuar...");
            Console.ReadKey();            
        }

        public static void ImprimirErro(Exception e)
        {
            ImprimirErro("Erro. " + e.Message);
        }
    }
}
