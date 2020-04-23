using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            try
            {               
                tab.colocarPeca(new Torre(tab, Cor.PRETA), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.PRETA), new Posicao(0, -3));

                Tela.ImprimirTabuleiro(tab);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
                       
            Console.ReadKey();
        }
    }
}
