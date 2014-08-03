using System;
using System.Collections.Generic;
using System.Text;

public class MNS
{
    public int combos(int[] numbers)
    {
        var distinct_magic_number_squares = new List<int[]>();
        var all_permutations = Permutations(numbers);

        foreach (var permutation in all_permutations)
        {
            int sum = permutation[0] + permutation[1] + permutation[2];

            // magic number square
            if (sum == permutation[3] + permutation[4] + permutation[5]
                && sum == permutation[6] + permutation[7] + permutation[8]
                && sum == permutation[0] + permutation[3] + permutation[6]
                && sum == permutation[1] + permutation[4] + permutation[7]
                && sum == permutation[2] + permutation[5] + permutation[8])
            {
                bool distinct = true;
                foreach (var list in distinct_magic_number_squares)
                {
                    bool equals = true;
                    for (int i = 0; i < 9; i++)
                    {
                        if (permutation[i] != list[i])
                        {
                            equals = false;
                            break;
                        }
                    }
                    if (equals)
                    {
                        distinct = false;
                        break;
                    }
                }
                if (distinct)
                    distinct_magic_number_squares.Add(permutation);
            }
        }


        return distinct_magic_number_squares.Count;
    }

    static List<int[]> Permutations(int[] list)
    {
        var ret = new List<int[]>();
        if (list.Length == 1)
            ret.Add(list);
        else
        {
            for (int i = 0; i < list.Length; i++)
            {
                int idx = 0;
                var rest = new int[list.Length - 1];// add each element to rest except the i-th
                for (int j = 0; j < list.Length; j++)
                {
                    if (j != i)
                        rest[idx++] = list[j];
                }

                foreach (var permutation in Permutations(rest))
                {
                    idx = 0;
                    var act = new int[list.Length];
                    act[idx++] = list[i];
                    for (int j = 0; j < permutation.Length; j++)
                    {
                        act[idx++] = permutation[j];
                    }
                    ret.Add(act);
                }
            }
        }
        return ret;
    }


    static void Main()
    {
        var start = DateTime.Now;
        for (int i = 0; i < 10; i++)
        {
            var solver = new MNS();
            Console.WriteLine(solver.combos(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }
        Console.WriteLine("Average runtime: " + (int)((DateTime.Now - start).TotalMilliseconds / 10));
    }

}
