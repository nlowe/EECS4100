using System;

namespace AutomataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var NFA = NonFiniteAutomata.parse(@"
3
ab
1
2
0
0 a 0
0 b 0
0 a 1
1 b 2
".Trim());

            var dfa = NFA.convertToDFA();

            Console.WriteLine("Hello World!");
        }
    }
}
