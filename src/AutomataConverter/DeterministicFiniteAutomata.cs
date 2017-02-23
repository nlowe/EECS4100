using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataConverter
{
    public class DeterministicFiniteAutomata : Automata
    {
        public DeterministicFiniteAutomata(int cardinality, string tokens, IEnumerable<int> acceptingStates, int startState, Dictionary<int, IEnumerable<Transition>> transitionMap)
            : base(cardinality, tokens, acceptingStates, startState, transitionMap)
        {
            foreach(var state in TransitionMap.Keys)
            {
                if(TransitionMap[state].Count() != ValidTokens.Length)
                    throw new ArgumentException(nameof(transitionMap), $"The transition map does not contain the correct number of transitions for state {state}. It should have exactly {ValidTokens.Length} transitions from it, otherwise it is not deterministic");
            }
        }
    }

    public static class NonFiniteAutomataExtensions
    {
        private class StateSet : List<int>
        {
            public StateSet(IEnumerable<int> states) : base(states)
            {
            }

            public override bool Equals(object other)
            {
                if(other is StateSet)
                {
                    return ((StateSet)other).OrderBy(x => x).SequenceEqual(this.OrderBy(x => x));
                }
                else
                {
                    return false;
                }
            }
        }

        private class IntermediateTransition
        {
            public readonly StateSet From;
            public readonly char Via;
            public readonly StateSet To;

            internal IntermediateTransition(StateSet from, char via, StateSet to)
            {
                From = from;
                Via = via;
                To = to;
            }
        }

        public static DeterministicFiniteAutomata convertToDFA(this NonFiniteAutomata nfa)
        {
            var newStartState = new StateSet(new List<int>{nfa.StartState});
            var visitedSets = new List<StateSet>();
            var toInspect = new Stack<StateSet>();

            toInspect.Push(newStartState);

            var intermediateTransitions = new List<IntermediateTransition>();

            do
            {
                var set = toInspect.Pop();
                visitedSets.Add(set);

                foreach(var c in nfa.ValidTokens)
                {
                    var reachableStates = new List<int>();

                    // Construct a state set of all reachable states from all states in the current set via c
                    foreach(var s in set)
                    {
                        if(!nfa.TransitionMap.ContainsKey(s)) continue;
                        reachableStates.AddRange(nfa.TransitionMap[s].Where(t => t.Via == c).Select(t => t.To));
                    }

                    // If we have not visited this state set yet push it onto the stack
                    var toState = new StateSet(reachableStates.Distinct());
                    if(!visitedSets.Contains(toState)) toInspect.Push(toState);

                    intermediateTransitions.Add(new IntermediateTransition(set, c, toState));
                }
            }while(toInspect.Count > 0);

            // Map each visited set of states to new IDs and build the new state set and transition map
            return new DeterministicFiniteAutomata(
                visitedSets.Count,
                nfa.ValidTokens,
                visitedSets.Select((s, i) => nfa.AcceptingStates.Intersect(s).Any() ? i : -1).Where(i => i > 0),
                0,
                intermediateTransitions.Select(t => new Transition(
                    visitedSets.IndexOf(t.From),
                    t.Via,
                    visitedSets.IndexOf(t.To)
                )).GroupBy(t => t.From).ToDictionary(k => k.Key, v => v.AsEnumerable())
            );
        }
    }
}