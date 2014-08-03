using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ZigZag
{
	public int longestZigZag(int[] sequence)
	{
		int[] longest_subsequence = new int[sequence.Length];
		int[] optimal_previous_index = new int[sequence.Length];
		int[] last_sign = new int[sequence.Length];
		for ( int i = 0 ; i < sequence.Length ; i++ )
		{
			last_sign[i] = 0;
			optimal_previous_index[i] = -1;
			longest_subsequence[i] = 1;
		}


		for ( int i = 1 ; i < sequence.Length ; i++ )
			for ( int j = 0 ; j < i ; j++ )
			{
				if ( sequence[i] == sequence[j] )
					continue;
				// if we get a greater subsequence and we can move from j to i.
				if ( longest_subsequence[j] + 1 > longest_subsequence[i] && 
					(last_sign[j] == 0 || Math.Sign( sequence[i] - sequence[j] ) != last_sign[j]) )
				{
					longest_subsequence[i] = longest_subsequence[j] + 1;
					last_sign[i] = Math.Sign( sequence[i] - sequence[j] );
					optimal_previous_index[i] = j;
				}
			}
		return longest_subsequence.Max();
	}



}
