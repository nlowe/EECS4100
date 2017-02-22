package AutomataConverter

import java.util.*

class NonFiniteAutomata(cardinality: Int,
                        validTokens: String,
                        acceptingStates: Array<Int>,
                        startState: Int,
                        transitionMap: List<Transition>
) : Automata(cardinality, validTokens, acceptingStates, startState, transitionMap) {
    companion object Parser
    {
        fun parse(text: String) : NonFiniteAutomata
        {
            val lines = text.trim().split("\n")

            var i = 0
            val cardinality = lines[i++].toInt()
            val validTokens = lines[i++]
            val acceptingCardinality = lines[i++].toInt()
            val acceptingStates = Array(acceptingCardinality, { 0 })

            var idx = 0
            while(idx < acceptingCardinality)
            {
                acceptingStates[idx++] = lines[i++].toInt()
            }

            val startState = lines[i++].toInt()
            val transitionMap = ArrayList<Transition>()
            while(i < lines.size)
            {
                val transition = lines[i++].split(" ")
                if(transition[1].length > 1) throw IllegalStateException("Transition state must be composed of a single character (got ${transition[1]})")

                val from = transition[0].toInt()
                val to = transition[2].toInt()

                transitionMap.add(
                        Transition(
                                State(from, isStartState = from == startState, isAcceptingState = acceptingStates.contains(from)),
                                transition[1][0],
                                State(to, isStartState = to == startState, isAcceptingState = acceptingStates.contains(to))
                        )
                )
            }

            return NonFiniteAutomata(cardinality, validTokens, acceptingStates, startState, transitionMap)
        }
    }
}