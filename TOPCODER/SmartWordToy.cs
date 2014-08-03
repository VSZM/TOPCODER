using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

public class SmartWordToy
{
    public int minPresses(String start, String finish, String[] forbid)
    {
        int level = 0;
        // Preprocessing rules for faster lookup
        var rules = new List<List<HashSet<char>>>();
        foreach (string rule in forbid)
        {
            var list = new List<HashSet<char>>();
            foreach (string word in rule.Split())
            {
                var hs = new HashSet<char>();
                foreach (char ch in word)
                {
                    hs.Add(ch);
                }
                list.Add(hs);
            }

            rules.Add(list);
        }

        HashSet<string> closed_nodes = new HashSet<string>(), open_nodes = new HashSet<string>();

        open_nodes.Add(start);

        while (open_nodes.Count > 0)
        {
            var next_level_open_nodes = new HashSet<string>();
            


            foreach (var actual in open_nodes)
            {

                if (actual.Equals(finish))
                    return level;

                // change each character backwards and forwards too.
                for (int i = 0; i < 4; i++)
                {
                    string next, prev;
                    char[] next_cha = actual.ToCharArray(), prev_cha = actual.ToCharArray();
                    char next_ch = (char)(actual[i] + 1), prev_ch = (char)(actual[i] - 1);

                    if (next_ch > 'z')
                        next_ch = 'a';
                    if (prev_ch < 'a')
                        prev_ch = 'z';

                    next_cha[i] = next_ch;
                    prev_cha[i] = prev_ch;
                    next = new string(next_cha);
                    prev = new string(prev_cha);


                    if (!closed_nodes.Contains(next) && !open_nodes.Contains(next))
                    {
                        bool valid = true;
                        foreach (var chars in rules)
                        {
                            if (chars[0].Contains(next[0]) && chars[1].Contains(next[1]) && chars[2].Contains(next[2]) && chars[3].Contains(next[3]))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            next_level_open_nodes.Add(next);
                        }
                    }

                    if (!closed_nodes.Contains(prev) && !open_nodes.Contains(prev))
                    {
                        bool valid = true;
                        foreach (var chars in rules)
                        {
                            if (chars[0].Contains(prev[0]) && chars[1].Contains(prev[1]) && chars[2].Contains(prev[2]) && chars[3].Contains(prev[3]))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            next_level_open_nodes.Add(prev);
                        }
                    }
                }

                closed_nodes.Add(actual);
            }

            ++level;
            open_nodes = next_level_open_nodes;
        }
        return -1;
    }
    /*
  static void Main()
    {
        DateTime start = DateTime.Now;
        var solver = new SmartWordToy();
        Console.WriteLine(solver.minPresses("zzzz", "aaaa", new string[] {"abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk",
 "abcdefghijkl abcdefghijkl abcdefghijkl abcdefghijk"}));
        
        Console.WriteLine( (int)(DateTime.Now - start).TotalMilliseconds + " ms runtime");  
  }*/
}