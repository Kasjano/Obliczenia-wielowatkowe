using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wątki
{
    internal class Macierz
    {
        internal int[,] GenerateRandomMatrix(int rows, int columns,int seed)
        {
            Random rand = new Random(seed);
            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rand.Next(0, 11);
                    //string formattedNumber = $"{matrix[i,j],2}";
                    //Console.Write(formattedNumber + " ");
                }
                //Console.WriteLine();
            }
            //Console.WriteLine();

            return matrix;
        }

        internal int[,] MnóżWiersze(int[,] matrixA, int[,] matrixB,int row)
        {
            int rowsA = matrixA.GetLength(0);
            int columnsA = matrixA.GetLength(1);
            int columnsB = matrixB.GetLength(1);

            int[,] result = new int[rowsA, columnsB];

            for (int j = 0; j < columnsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < columnsA; k++)
                {
                    sum += matrixA[row, k] * matrixB[k, j];
                }
                result[row, j] = sum;
            }

            return result;
        }

        internal int[,] MnóżMacierz(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int columnsA = matrixA.GetLength(1);
            int columnsB = matrixB.GetLength(1);

            int[,] result = new int[rowsA, columnsB];

            for (int i = 0; i < columnsB; i++)
            {
                for (int j = 0; j < columnsB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < columnsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                    //string formattedNumber = $"{sum,5}";
                    //Console.Write(formattedNumber + " ");
                }
                //Console.WriteLine();
            }
            //Console.WriteLine();

            return result;
        }

        internal void WypiszMacierzWynikową(int size, int[,] result)
        {
            Console.WriteLine("Macierz Wynikowa:");
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    string formattedNumber = $"{result[r, c],5}";
                    Console.Write(formattedNumber + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
