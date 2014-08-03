using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CaptureThemAll
{
    public class State : IEquatable<State>, IComparable<State>
    {
        public bool IsQueenCaptured { get; private set; }
        public bool IsRookCaptured { get; private set; }
        public int num_steps { get; private set; }

        public char row { get; private set; }
        public char col { get; private set; }

        public State(bool queen, bool rook, int steps, char r, char c)
        {
            this.IsQueenCaptured = queen;
            this.IsRookCaptured = rook;
            this.num_steps = steps;
            this.row = r;
            this.col = c;
        }

        public bool Equals(State other)
        {
            return this.row.Equals(other.row) && this.col.Equals(other.col) && this.IsQueenCaptured == other.IsQueenCaptured && this.IsRookCaptured == other.IsRookCaptured;
        }

        public int CompareTo(State other)
        {
            if (this.Equals(other))
                return 0;
            return -1;
        }
    }

    public int fastKnight(String knight, String rook, String queen)
    {
        Queue<State> open_nodes = new Queue<State>();
        HashSet<State> closed_nodes = new HashSet<State>();

        open_nodes.Enqueue(new State(false, false, 0, knight[0], knight[1]));
        int[] knight_row_steps = new int[] { +1, -1, +1, -1, +2, +2, -2, -2 },
              knight_col_steps = new int[] { +2, +2, -2, -2, +1, -1, +1, -1 };


        while (open_nodes.Count > 0)
        {
            var actual = open_nodes.Dequeue();

            if (actual.IsQueenCaptured && actual.IsRookCaptured)
                return actual.num_steps;

            for (int i = 0; i < knight_col_steps.Length; i++)
            {
                char new_row = (char)(actual.row + knight_row_steps[i]), new_col = (char)(actual.col + knight_col_steps[i]);
                
                // check if we are still inside the table
                if (new_row >= 'a' && new_row <= 'h' && new_col >= '1' && new_col <= '8')
                {
                    var new_state = new State(actual.IsQueenCaptured || (new_row == queen[0] && new_col == queen[1]),
                                                actual.IsRookCaptured || (new_row == rook[0] && new_col == rook[1]), actual.num_steps + 1,
                                                new_row, new_col);
                    if (!closed_nodes.Contains(new_state) && !open_nodes.Contains(new_state))
                        open_nodes.Enqueue(new_state);
                }
            }

            closed_nodes.Add(actual);
        }

        return 0;
    }

   /* static void Main()
    {
        var solver = new CaptureThemAll();
        Console.WriteLine(solver.fastKnight("a8","h8","a1"));
    }*/
}
