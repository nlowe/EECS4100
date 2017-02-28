using System;
using System.IO;

namespace AutomataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1 || args.Length > 2)
            {
                PrintHelp();
                Environment.Exit(-1);
            }

            if(!File.Exists(args[0]))
            {
                Console.WriteLine($"Could not open file for read: '{args[0]}'");
                Environment.Exit(-1);
            }

            try
            {
                var nfaSource = File.ReadAllText(args[0]).Trim();
                var nfa = NonFiniteAutomata.parse(nfaSource);

                if(args.Length == 2 && args[1].ToLower() == "-v")
                {
                    Console.WriteLine("Parsed NFA:");
                    Console.WriteLine(nfa.ToString());
                    Console.WriteLine("\nDFA:\n");
                }

                var dfa = nfa.convertToDFA();

                Console.WriteLine(dfa.ToString());
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert DFA: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);

                Environment.Exit(-2);
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Syntax: dotnet AutomataConverter.dll <file> [-v]");
        }
    }
}
