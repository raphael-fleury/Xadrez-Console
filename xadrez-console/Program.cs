﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.colocarPeca(new Rei(tab, Cor.BRANCA), new Posicao(0, 0));
            tab.colocarPeca(new Torre(tab, Cor.PRETA), new Posicao(0, 1));

            Tela.ImprimirTabuleiro(tab);

            Console.ReadKey();
        }
    }
}
