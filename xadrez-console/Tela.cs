using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for(int y = 0; y < tab.linhas; y++)
            {
                for (int x = 0; x < tab.colunas; x++)
                {
                    Console.Write(tab.peca(y, x) != null ? tab.peca(y, x) + " " : "- ");
                }
                Console.WriteLine();
            }
        }
    }
}
