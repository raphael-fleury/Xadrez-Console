using System;
using tabuleiro;
using xadrez;

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

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            return new PosicaoXadrez(s[0], int.Parse(s[1] + ""));
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
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erro);
            Console.ForegroundColor = defaultColor;
        }

        public static void ImprimirErro(Exception e)
        {
            ImprimirErro("Erro. " + e.Message);
        }
    }
}
