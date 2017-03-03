using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataConverter
{
    /// <summary>
    /// A collection of states and transitions with one or more accepting states
    /// </summary>
    public abstract class Automata
    {
        /// <summary>
        /// The number of states in the automata. All states are identified by
        /// the positive integers starting from zero
        /// </summary>
        public readonly int Cardinality;

        /// <summary>
        /// The valid tokens accepted by the automata. Each character in the
        /// string is a valid token
        /// </summary>
        public readonly string ValidTokens;
        
        /// <summary>
        /// All accepting states in the automata
        /// </summary>
        public readonly IEnumerable<int> AcceptingStates;
        
        /// <summary>
        /// The initial state for the automata
        /// </summary>
        public readonly int StartState;

        /// <summary>
        /// All transitions in the automata, keyed by source state
        /// </summary>
        public readonly Dictionary<int, IEnumerable<Transition>> TransitionMap;

        protected Automata(int cardinality, string tokens, IEnumerable<int> acceptingStates, int startState, Dictionary<int, IEnumerable<Transition>> transitionMap)
        {
            if(cardinality < 0) throw new ArgumentException("The cardinality must be positive", nameof(cardinality));
            if(startState < 0 || startState >= cardinality) throw new ArgumentException("Invalid start state. Must be in [0,cardinality)", nameof(startState));
            if(acceptingStates.Count() == 0) throw new ArgumentException("The automata must have at least one accepting state", nameof(acceptingStates));
            if(string.IsNullOrWhiteSpace(tokens)) throw new ArgumentException("The set of valid tokens cannot be empty", nameof(tokens));

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
            var states = string.Join(" ", AcceptingStates);
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

