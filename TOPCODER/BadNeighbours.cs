using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BadNeighbors
{

	public int maxDonations(int[] donations)
	{
		int[] max = new int[donations.Length];
		int variation_1, variation_2;

		max[0] = donations[0];
		max[1] = Math.Max( donations[0], donations[1] );
		for ( int i = 2 ; i < donations.Length - 1 ; i++ )
		{
			max[i] = Math.Max( max[i - 1], donations[i] + max[i - 2] );
		}
		variation_1 = max[donations.Length - 2];
		if ( donations.Length > 2 )
		{

			max[1] = donations[1];
			max[2] = Math.Max( donations[1], donations[2] );
			for ( int i = 3 ; i < donations.Length ; i++ )
			{
				max[i] = Math.Max( max[i - 1], donations[i] + max[i - 2] );
			}
			variation_2 = max[donations.Length - 1];
		}
		else
			variation_2 = donations[1];


		return Math.Max( variation_1, variation_2 );
	}

	/*static void Main()
	{
		var solver = new BadNeighbors();
		solver.maxDonations( new int[] { 11, 15 } );
	}*/

}

