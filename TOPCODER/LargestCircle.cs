using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LargestCircle
{
    public int radius(String[] grid)
    {
        int height = grid.Length, width = grid[0].Length, max = 0;


        for (int row = 1; row < height; row++)
        {
            for (int col = 1; col < width; col++)
            {
                double row_to_grid = row - 0.5, col_to_grid = col - 0.5;
                int radius = 1;
                while (row - radius >= 0 && row + radius <= height && col - radius >= 0 && col + radius <= width) // inside grid
                {
                    bool does_not_contain_marked = true;

                    // check for # among the cells that are radius distance away from current position.
                    for (int r = 0; r < height; r++)
                    {
                        for (int c = 0; c < width; c++)
                        {
                            if (grid[r][c] == '#' && (int)Math.Round(Math.Sqrt((row_to_grid - r) * (row_to_grid - r) + (col_to_grid - c) * (col_to_grid - c))) == radius)
                            {
                                does_not_contain_marked = false;
                                goto broken;
                            }
                        }
                    }


                broken:
                    if (does_not_contain_marked && radius > max)
                    {
                        max = radius;
                    }
                    radius++;
                }
            }
        }

        return max;
    }

}
