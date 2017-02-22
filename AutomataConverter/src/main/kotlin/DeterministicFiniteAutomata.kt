package AutomataConverter

import java.util.*

private data class StateSet(val states: Collection<State>){
    val isAcceptingState: Boolean = states.any {it.isAcceptingState}

    override fun equals(other: Any?): Boolean {
        return if (other is StateSet) states.size == other.states.size && states.containsAll(other.states) else false
    }

    override fun hashCode(): Int {
        return states.hashCode()
    }
}

private data class IntermediateTransition(val from: StateSet, val via: Char, val to: StateSet)

class DeterministicFiniteAutomata(cardinality: Int,
                        validTokens: String,
                        acceptingStates: Array<Int>,
                        startState: Int,
                        transitionMap: List<Transition>
) : Automata(cardinality, validTokens, acceptingStates, startState, transitionMap) {
    companion object Converter{
        fun fromNFA(nfa: NonFiniteAutomata): DeterministicFiniteAutomata {
            val newStartState = StateSet(listOf(State(nfa.startState, isStartState = true)))
            val visitedSets = ArrayList<StateSet>()
            val toInspect = Stack<StateSet>()
            toInspect.push(newStartState)

            val intermediateTransitions = ArrayList<IntermediateTransition>()

            do
            {
                val set = toInspect.pop()
                visitedSets.add(set)

                for(c in nfa.validTokens)
                {
                    val reachableStates = ArrayList<State>()
                    // Construct a state set of all reachable states from all states in the current set via c
                    for(s in set.states)
                    {
                        nfa.transitionMap.filter { it.from == s && it.via == c }.forEach { reachableStates.add(it.to) }
                    }

                    // If we have not visited this state set yet push it onto the stack
                    val toState = StateSet(reachableStates.distinctBy { it.id })
                    if(!visitedSets.contains(toState)) toInspect.push(toState)

                    intermediateTransitions.add(IntermediateTransition(set, c, toState))
                }
            }while(toInspect.isNotEmpty())

            // Map each visited set of states to new IDs and build the new state set and transition map
            return DeterministicFiniteAutomata(
                    visitedSets.size,
                    nfa.validTokens,
                    visitedSets.mapIndexed { i, stateSet -> if(stateSet.isAcceptingState) i else -1 }.filter { it > 0 }.toTypedArray(),
                    0,
                    intermediateTransitions.map {
                        Transition(
                                State(
                                        visitedSets.indexOf(it.from)
                                ),
                                it.via,
                                State(
                                        visitedSets.indexOf(it.to)
                                )
                        )
                    }
            )
        }
    }
}