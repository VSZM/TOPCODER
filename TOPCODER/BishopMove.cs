using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BishopMove
{
    public int howManyMoves(int r1, int c1, int r2, int c2)
    {
        int iterations = 0;
        int[,] table = new int[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                table[i, j] = -1;
            }
        }

        table[r1, c1] = 0;

        while (table[r2, c2] < 0 && iterations < 25)
        {
            int[,] buf_table = new int[8, 8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    buf_table[row, col] = table[row, col];
                }
            }

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {

                    if (table[row, col] >= 0)
                    {
                        for (int k = 1; k < 8; k++)
                        {
                            if ((row + k < 8 && col + k < 8) && (buf_table[row + k, col + k] > table[row, col] + 1 || buf_table[row + k, col + k] == -1)) buf_table[row + k, col + k] = table[row, col] + 1;
                            if ((row + k < 8 && col - k >= 0) && (buf_table[row + k, col - k] > table[row, col] + 1 || buf_table[row + k, col - k] == -1)) buf_table[row + k, col - k] = table[row, col] + 1;
                            if ((row - k >= 0 && col + k < 8) && (buf_table[row - k, col + k] > table[row, col] + 1 || buf_table[row - k, col + k] == -1)) buf_table[row - k, col + k] = table[row, col] + 1;
                            if ((row - k >= 0 && col - k >= 0) && (buf_table[row - k, col - k] > table[row, col] + 1 || buf_table[row - k, col - k] == -1)) buf_table[row - k, col - k] = table[row, col] + 1;
                        }
                    }
                }
            }


            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    table[row, col] = buf_table[row, col];
                }
            }

            ++iterations;
        }

      /**  for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Console.Write(table[row,col] + "\t");
            }
            Console.WriteLine();
        }*/

        return table[r2, c2];
    }
/*
    static void Main()
    {
        var solver = new BishopMove();
        Console.WriteLine( solver.howManyMoves(0, 0, 7, 7));
    }*/
}
