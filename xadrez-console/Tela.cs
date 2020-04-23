using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int y = 0; y < tab.linhas; y++)
            {
                Console.Write(tab.linhas - y + " ");
                for (int x = 0; x < tab.colunas; x++)
                {
                    if (tab.peca(y, x) == null)
                        Console.Write("-");
                    else
                        ImprimirPeca(tab.peca(y, x));

                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            switch (peca.cor)
            {
                case Cor.BRANCA:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Cor.PRETA:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
            Console.Write(peca);
            Console.ForegroundColor = defaultColor;
        }
    }
}
