using System.Threading;
using System;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Wątki
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 500;
            int seed = 2;
            int seed2 = 3;
            int n = 5;
            Macierz macierz = new Macierz();
            Console.WriteLine("Macierz A:");
            int[,] macierzA = macierz.GenerateRandomMatrix(size, size,seed);
            Console.WriteLine("Macierz B:");
            int[,] macierzB = macierz.GenerateRandomMatrix(size, size,seed2);
            int[,] result = new int[size, size];
            int[,] result2 = new int[size, size];

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Parallel.For(0, size, new ParallelOptions { MaxDegreeOfParallelism = n }, row =>
            {
                int[,] row_result = macierz.MnóżWiersze(macierzA, macierzB,row);
                for (int col = 0; col < size; col++)
                {
                    result[row, col] = row_result[row, col];
                }
            });
            watch.Stop();
            Console.WriteLine($"Parallel ended in {watch.ElapsedMilliseconds} ms.");
            Console.WriteLine("Z użyciem Parallel:");
            //macierz.WypiszMacierzWynikową(size,result);

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            Thread[] threads = new Thread[n];
            for (int i = 0; i < n; i++)
            {
                int threadIndex = i;
                threads[i] = new Thread(() =>
                {
                    for (int row = threadIndex; row < size; row += n)
                    {
                        int[,] row_result = macierz.MnóżWiersze(macierzA, macierzB, row);
                        for (int c = 0; c < size; c++)
                        {
                            result2[row, c] = row_result[row, c];
                        }
                    }
                });
                threads[i].Start();
            }

            foreach (Thread x in threads) x.Join();
            Console.WriteLine();
            watch2.Stop();
            Console.WriteLine($"Thread ended in {watch2.ElapsedMilliseconds} ms.");
            Console.WriteLine("Z użyciem Thread:");
            //macierz.WypiszMacierzWynikową(size, result2);

            Console.WriteLine();
            var watch3 = System.Diagnostics.Stopwatch.StartNew();
            macierz.MnóżMacierz(macierzA, macierzB);
            watch3.Stop();
            Console.WriteLine($"Ended in {watch3.ElapsedMilliseconds} ms.");
        }
    }
}
