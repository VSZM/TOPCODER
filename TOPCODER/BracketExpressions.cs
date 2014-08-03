using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BracketExpressions
{
    static char[] opening_brackets = new char[3] { '(', '[', '{' };
    static char[] closing_brackets = new char[3] { ')', ']', '}' };


    public string ifPossible(string expression)
    {
        return Is_Special(expression.ToCharArray()) ? "possible" : "impossible";
    }

    public bool Is_Special(char[] str)
    {
        if (str.Length == 0) return true;

        int x_idx = new string(str).IndexOf('X');
        if (x_idx >= 0)
        {
            char[] a = new char[str.Length], b = new char[str.Length], c = new char[str.Length], d = new char[str.Length], e = new char[str.Length], f = new char[str.Length];
            Array.Copy(str, a,str.Length);
            Array.Copy(str, b, str.Length);
            Array.Copy(str, c, str.Length);
            Array.Copy(str, d, str.Length);
            Array.Copy(str, e, str.Length);
            Array.Copy(str, f, str.Length);
            
            a[x_idx] = '(';
            b[x_idx] = ')';
            c[x_idx] = '{';
            d[x_idx] = '}';
            e[x_idx] = '[';
            f[x_idx] = ']';
            return Is_Special(a) || Is_Special(b) || Is_Special(c) || Is_Special(d) || Is_Special(e) || Is_Special(f);
        }
        Stack<char> stack = new Stack<char>();

        for (int i = 0; i < str.Length; i++)
        {
            if (opening_brackets.Contains(str[i]))
                stack.Push(str[i]);
            else
            {
                if (stack.Count == 0)
                    return false;

                int idx_a = 0, idx_b = 0;
                while(opening_brackets[idx_a] != stack.Peek())
                    idx_a++;

                while (closing_brackets[idx_b] != str[i])
                    idx_b++;

                if (idx_a == idx_b)
                    stack.Pop();
                else
                    return false;
            }
        }

        return stack.Count == 0;
    }

}
