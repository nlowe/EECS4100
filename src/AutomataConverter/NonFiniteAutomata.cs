using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutomataConverter
{
    public class NonFiniteAutomata : Automata
    {
        public NonFiniteAutomata(int cardinality, string tokens, IEnumerable<int> acceptingStates, int startState, Dictionary<int, IEnumerable<Transition>> transitionMap)
            : base(cardinality, tokens, acceptingStates, startState, transitionMap)
        {
        }

        public static NonFiniteAutomata parse(string source)
        {
            var lines = new Regex("\\s+").Split(source.Trim());

            var i = 0;
            var cardinality = int.Parse(lines[i++]);
            var validTokens = lines[i++];
            var acceptingCardinality = int.Parse(lines[i++]);
            var acceptingStates = new List<int>(acceptingCardinality);

            for(var idx = 0; idx < acceptingCardinality; idx++)
            {
                acceptingStates.Add(int.Parse(lines[i++]));
            }

            var startState = int.Parse(lines[i++]);
            var transitionMap = new List<Transition>();
            while(i < lines.Length)
            {
                var from = int.Parse(lines[i++]);
                var via = lines[i++];

                if(via.Length > 1) throw new ArgumentOutOfRangeException("via", "Transition string cannot be more than one character");

                var to = int.Parse(lines[i++]);

                transitionMap.Add(new Transition(from, via[0], to));
            }

            return new NonFiniteAutomata(cardinality, validTokens, acceptingStates, startState, transitionMap.GroupBy(t => t.From).ToDictionary(k => k.Key, v => v.AsEnumerable()));
        }
    }
}
