using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareExpr;

namespace CompareExprTest
{
    class Program
    {
        static Dictionary<string, List<string>> pivots;
        static Dictionary<string, Tuple<string, string>> pivot_names;

        static bool GetChoice(string item, out string choice, out string pivotName)
        {
            Tuple<string, string> res;
            if (pivot_names.TryGetValue(item.ToLowerInvariant(), out res))
            {
                choice = res.Item1;
                pivotName = res.Item2;
                return true;
            }
            choice = pivotName = "";
            return false;
        }

        static void Main(string[] args)
        {
            pivots = new Dictionary<string,List<string>>();
            pivot_names = new Dictionary<string, Tuple<string, string>>();
            while (true)
            {
                var line = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(line) || line.ToLower() == "quit" || line.ToLower() == "exit")
                    break;

                if (line.StartsWith("pivot "))
                {
                    var parts = line.Split(' ');
                    if (parts.Length < 4)
                    {
                        Console.WriteLine("pivot must have a name and at least 2 values");
                        continue;
                    }
                    List<string> values = new List<string>(parts.Length-2);
                    for (int i = 2; i < parts.Length; i++)
                    {
                        values.Add(parts[i]);
                        pivot_names[parts[i].ToLowerInvariant()] = Tuple.Create(parts[i], parts[1]);
                    }
                    pivots[parts[1]] = values;
                    continue;
                }

                if (line.StartsWith("eval "))
                {
                    PivotsExpression expr = PivotsExpression.ReadExpression(pivots, new PivotsExpression.GetChoiceDelegate(GetChoice), line.Substring(5));
                    if (expr.IsInvariant)
                    {
                        if (expr.IsAlwaysTrue)
                            Console.WriteLine("true");
                        else
                            Console.WriteLine("false");
                    }
                    else
                    {
                        foreach (var combination in expr.GetCombinations())
                        {
                            Console.Write("    [");
                            foreach (var choice in combination)
                            {
                                Console.Write(choice);
                                Console.Write(",");
                            }
                            Console.WriteLine("]");
                        }
                    }
                    continue;
                }

                Console.WriteLine("unrecognized command");
            }
        }
    }
}
