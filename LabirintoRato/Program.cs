using System;
using System.Collections.Generic;

namespace LabirintoDoRato
{
    internal class LabirintoDoRato
    {
        private const int tamanho = 18;

        static void ExibirLabirinto(char[,] labirinto, int linhas, int colunas)
        {
            for (int i = 0; i < linhas; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < colunas; j++)
                {
                    Console.Write($"{labirinto[i, j]} ");
                }
            }
            Console.WriteLine("\nProcurando o queijo!");
        }

        static void InicializarLabirinto(char[,] labirinto)
        {
            Random rnd = new Random();
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    labirinto[i, j] = rnd.Next(4) == 1 ? '*' : ' ';
                }
            }

            for (int i = 0; i < tamanho; i++)
            {
                labirinto[0, i] = '*';
                labirinto[tamanho - 1, i] = '*';
                labirinto[i, 0] = '*';
                labirinto[i, tamanho - 1] = '*';
            }

            int x = rnd.Next(tamanho);
            int y = rnd.Next(tamanho);
            labirinto[x, y] = 'Q';
        }

        static void ProcurarQueijo(char[,] labirinto, int linha, int coluna)
        {
            Stack<int> caminho = new Stack<int>();
            int linhaAnterior = 0, colunaAnterior = 0;

            do
            {
                labirinto[linha, coluna] = 'r';
                linhaAnterior = linha;
                colunaAnterior = coluna;

                if (labirinto[linha - 1, coluna] == 'Q' || labirinto[linha + 1, coluna] == 'Q' || labirinto[linha, coluna + 1] == 'Q' || labirinto[linha, coluna - 1] == 'Q')
                {
                    if (labirinto[linha - 1, coluna] == 'Q')
                    {
                        linha--;
                    }
                    else if (labirinto[linha + 1, coluna] == 'Q')
                    {
                        linha++;
                    }
                    else if (labirinto[linha, coluna + 1] == 'Q')
                    {
                        coluna++;
                    }
                    else if (labirinto[linha, coluna - 1] == 'Q')
                    {
                        coluna--;
                    }

                    System.Threading.Thread.Sleep(300);
                    Console.Clear();
                    ExibirLabirinto(labirinto, tamanho, tamanho);
                    labirinto[linhaAnterior, colunaAnterior] = '.';

                    linhaAnterior = linha;
                    colunaAnterior = coluna;
                    labirinto[linha, coluna] = 'r';
                    System.Threading.Thread.Sleep(300);
                    Console.Clear();
                    ExibirLabirinto(labirinto, tamanho, tamanho);

                    Console.WriteLine("O rato encontrou o queijo!");
                    break;
                }
                else if (labirinto[linha, coluna + 1] == ' ')
                {
                    caminho.Push(linha);
                    caminho.Push(coluna);
                    coluna++;
                }
                else if (labirinto[linha + 1, coluna] == ' ')
                {
                    caminho.Push(linha);
                    caminho.Push(coluna);
                    linha++;
                }
                else if (labirinto[linha, coluna - 1] == ' ')
                {
                    caminho.Push(linha);
                    caminho.Push(coluna);
                    coluna--;
                }
                else if (labirinto[linha - 1, coluna] == ' ')
                {
                    caminho.Push(linha);
                    caminho.Push(coluna);
                    linha--;
                }
                else if (caminho.Count > 0)
                {
                    coluna = caminho.Pop();
                    linha = caminho.Pop();
                }
                else
                {
                    Console.WriteLine("O queijo é impossível de ser encontrado.");
                    break;
                }

                System.Threading.Thread.Sleep(300);
                Console.Clear();
                ExibirLabirinto(labirinto, tamanho, tamanho);
                labirinto[linhaAnterior, colunaAnterior] = '.';

            } while (true);
        }

        public static void Main(String[] args)
        {
            char[,] labirinto = new char[tamanho, tamanho];
            InicializarLabirinto(labirinto);
            ExibirLabirinto(labirinto, tamanho, tamanho);
            ProcurarQueijo(labirinto, 1, 1);
            Console.ReadKey();
        }
    }
}
