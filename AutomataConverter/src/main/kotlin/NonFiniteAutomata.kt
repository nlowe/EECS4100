package AutomataConverter;

import java.util.*

fun String.toNonFiniteAutomata(): NonFiniteAutomata {
    val lines = this.trim().split("\n")

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
        transitionMap.add(Transition(transition[0].toInt(), transition[1][0], transition[2].toInt()))
    }

    return NonFiniteAutomata(cardinality, validTokens, acceptingStates, startState, transitionMap)
}

class NonFiniteAutomata(cardinality: Int,
                        validTokens: String,
                        acceptingStates: Array<Int>,
                        startState: Int,
                        transitionMap: List<Transition>
) : FiniteAutomata(cardinality, validTokens, acceptingStates, startState, transitionMap){
    override fun repr(): String {
        return """
$cardinality
$validTokens
${acceptingStates.size}
${acceptingStates.joinToString(separator = "\n")}
$startState
${transitionMap.joinToString(separator = "\n")}
""".trim()
    }
}