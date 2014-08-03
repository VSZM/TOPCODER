using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ChessMetric
{
	public long howMany(int size, int[] start, int[] end, int numMoves)
	{
		long[,] board = new long[size, size];
		int[] row_mov = new int[16] { -1, +1, 0, 0, -1, +1, +1, -1, +2, +2, -2, -2, +1, -1, +1, -1 },
			  col_mov = new int[16] { 0, 0, -1, +1, -1, +1, -1, +1, +1, -1, +1, -1, +2, +2, -2, -2 };


		// First iteration, we move from start
		for ( int i = 0 ; i < 16 ; i++ )
		{
			int new_x = row_mov[i] + start[0], new_y = col_mov[i] + start[1];
			if ( new_x >= 0 && new_x < size && new_y >= 0 && new_y < size )
				board[new_x, new_y] += 1;
		}

		for ( int i = 1 ; i < numMoves ; i++ )
		{
			long[,] buf_board = new long[size, size];
			for ( int row = 0 ; row < size ; row++ )
			{
				for ( int col = 0 ; col < size ; col++ )
				{
					buf_board[row, col] += board[row, col];

					// move to every direction from current cell.
					for ( int j = 0 ; j < 16 ; j++ )
					{
						int new_row = row_mov[j] + row, new_col = col_mov[j] + col;
						// we must kepp it inside the board
						if ( board[row, col] > 0 && new_row >= 0 && new_row < size && new_col >= 0 && new_col < size )
							buf_board[new_row, new_col] += board[row, col] ;
					}
				}
			}

			for ( int row = 0 ; row < size ; row++ )
			{
				for ( int col = 0 ; col < size ; col++ )
				{
					board[row, col] = buf_board[row, col];
				}
			}
		}


		return board[end[0], end[1]];
	}


	/*static void Main()
	{
		var solver = new ChessMetric();
		Console.WriteLine( solver.howMany(

100
,
new int[] { 0, 0 }
		,
new int[] { 0, 99 }
		,
50 ) );
	}*/
}
