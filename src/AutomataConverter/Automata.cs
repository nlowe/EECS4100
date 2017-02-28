using System.Collections.Generic;
using System.Linq;

namespace AutomataConverter
{
    public abstract class Automata
    {
        public readonly int Cardinality;
        public readonly string ValidTokens;
        public readonly IEnumerable<int> AcceptingStates;
        public readonly int StartState;
        public readonly Dictionary<int, IEnumerable<Transition>> TransitionMap;

        protected Automata(int cardinality, string tokens, IEnumerable<int> acceptingStates, int startState, Dictionary<int, IEnumerable<Transition>> transitionMap)
        {
            Cardinality = cardinality;
            ValidTokens = tokens;
            AcceptingStates = acceptingStates.ToList();
            StartState = startState;
            
            TransitionMap = new Dictionary<int, IEnumerable<Transition>>();
            foreach(var t in transitionMap)
            {
                TransitionMap.Add(t.Key, t.Value.ToList());
            }
        }

        public override string ToString()
        {
            var states = string.Join("\n", AcceptingStates);
            var transitions = string.Join("\n", TransitionMap.SelectMany(t => t.Value));

            return $@"
{Cardinality}
{ValidTokens}
{AcceptingStates.Count()} {states}
{StartState}
{transitions}
".Trim();
        }
    }
}

