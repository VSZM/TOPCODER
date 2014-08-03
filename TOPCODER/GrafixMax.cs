using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
public class grafixMask
{
	private int img_idx = 0;
	private const int width = 600;
	private const int height = 400;

	private struct Coordinates : IComparable<Coordinates>, IEquatable<Coordinates>
	{
		public int row;
		public int column;
		public Coordinates(int _row, int _col)
		{
			this.row = _row;
			this.column = _col;
		}


		public int CompareTo(Coordinates other)
		{
			if ( row.CompareTo( other.row ) == 0 )
			{
				return column.CompareTo( other.column );
			}
			return row.CompareTo( other.row );
		}

		public bool Equals(Coordinates other)
		{
			return this.row == other.row && this.column == other.column;
		}
	}

	private UInt16 actual_color;


	public int[] sortedAreas(string[] rectangles)
	{
		#region Field_initialization
		actual_color = 2;
		// 0 traversable, 1 blocked,distinct traversable areas are indexed by 2 
		UInt16[,] field = new UInt16[height, width];
		for ( int i = 0 ; i < rectangles.Length ; i++ )
		{
			string[] coordinates = rectangles[i].Split();
			int upper_left_x = int.Parse( coordinates[0] ), upper_left_y = int.Parse( coordinates[1] ), lower_right_x = int
				.Parse( coordinates[2] ), lower_right_y = int.Parse( coordinates[3] );

			for ( int row = upper_left_x ; row <= lower_right_x ; row++ )
			{
				for ( int col = upper_left_y ; col <= lower_right_y ; col++ )
				{
					field[row, col] = 1;
				}
			}
		}
		#endregion
		Save_Image( field );


		List<int> free_areas_sizes = new List<int>();

		for ( int row = 0 ; row < height ; row++ )
		{
			for ( int col = 0 ; col < width ; col++ )
			{
				if ( field[row, col] == 0 )
					free_areas_sizes.Add( Flood_Fill( field, new Coordinates( row, col ) ) );
			}
		}



		Save_Image( field );

		free_areas_sizes.Sort();
		return free_areas_sizes.ToArray();
	}

	private void Save_Image(UInt16[,] field)
	{
		Bitmap image = new Bitmap( width, height );

		var colors = new Color[actual_color];
		colors[0] = Color.White;// empty is white
		colors[1] = Color.Black;// blocked is black

		for ( int i = 2 ; i < actual_color ; i++ )
		{
			ulong val =((ulong) i * 256ul * 256ul * 256ul) / (ulong)actual_color;
			colors[i] = Color.FromArgb( ( byte )((val % (256 * 256 * 256)) / (256 * 256)), ( byte )((val % (256 * 256)) / 256), ( byte )val % 256 );
		}


		for ( int sor = 0 ; sor < height ; sor++ )
		{
			for ( int oszlop = 0 ; oszlop < width ; oszlop++ )
			{
				image.SetPixel( oszlop, sor, colors[field[sor, oszlop]] );
			}
		}

		image.Save( "output_" + img_idx++ + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp );
	}

	private void Print_Field(UInt16[,] field)
	{
		for ( int row = 0 ; row < height ; row++ )
		{
			for ( int col = 0 ; col < width ; col++ )
			{
				Console.Write( field[row, col] );
			}
			Console.WriteLine();
		}
	}

	private int Flood_Fill(UInt16[,] field, Coordinates start_coordinates)
	{
		HashSet<Coordinates> already_added_to_processing = new HashSet<Coordinates>();
		Queue<Coordinates> processing_queue = new Queue<Coordinates>();


		processing_queue.Enqueue( start_coordinates );
		already_added_to_processing.Add( start_coordinates );


		while ( processing_queue.Count > 0 )
		{
			var actual = processing_queue.Dequeue();

			// top neighbour
			if ( actual.row > 0 && field[actual.row - 1, actual.column] == 0 )
			{
				var new_coords = new Coordinates( actual.row - 1, actual.column );
				if ( !already_added_to_processing.Contains( new_coords ) )
				{
					processing_queue.Enqueue( new_coords );
					already_added_to_processing.Add( new_coords );
				}
			}
			// bottom neighbour
			if ( actual.row < height - 1 && field[actual.row + 1, actual.column] == 0 )
			{
				var new_coords = new Coordinates( actual.row + 1, actual.column );
				if ( !already_added_to_processing.Contains( new_coords ) )
				{
					processing_queue.Enqueue( new_coords );
					already_added_to_processing.Add( new_coords );
				}
			}
			// left neighbour
			if ( actual.column > 0 && field[actual.row, actual.column - 1] == 0 )
			{
				var new_coords = new Coordinates( actual.row, actual.column - 1 );
				if ( !already_added_to_processing.Contains( new_coords ) )
				{
					processing_queue.Enqueue( new_coords );
					already_added_to_processing.Add( new_coords );
				}
			}
			// right neighbour
			if ( actual.column < width - 1 && field[actual.row, actual.column + 1] == 0 )
			{
				var new_coords = new Coordinates( actual.row, actual.column + 1 );
				if ( !already_added_to_processing.Contains( new_coords ) )
				{
					processing_queue.Enqueue( new_coords );
					already_added_to_processing.Add( new_coords );
				}
			}

			field[actual.row, actual.column] = actual_color;
		}


		actual_color++;
		return already_added_to_processing.Count;
	}

	/*public static void Main()
	{
		var solver = new grafixMask();
		solver.sortedAreas( new string[]{"0 20 399 20", "0 44 399 44", "0 68 399 68", "0 92 399 92",
 "0 116 399 116", "0 140 399 140", "0 164 399 164", "0 188 399 188",
 "0 212 399 212", "0 236 399 236", "0 260 399 260", "0 284 399 284",
 "0 308 399 308", "0 332 399 332", "0 356 399 356", "0 380 399 380",
 "0 404 399 404", "0 428 399 428", "0 452 399 452", "0 476 399 476",
 "0 500 399 500", "0 524 399 524", "0 548 399 548", "0 572 399 572",
 "0 596 399 596", "5 0 5 599", "21 0 21 599", "37 0 37 599",
 "53 0 53 599", "69 0 69 599", "85 0 85 599", "101 0 101 599",
 "117 0 117 599", "133 0 133 599", "149 0 149 599", "165 0 165 599",
 "181 0 181 599", "197 0 197 599", "213 0 213 599", "229 0 229 599",
 "245 0 245 599", "261 0 261 599", "277 0 277 599", "293 0 293 599",
 "309 0 309 599", "325 0 325 599", "341 0 341 599", "357 0 357 599",
 "373 0 373 599", "389 0 389 599"} );
	}*/
}
