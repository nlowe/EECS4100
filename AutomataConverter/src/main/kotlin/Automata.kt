package AutomataConverter

data class Transition(val from: State, val via: Char, val to: State) {
    override fun toString(): String {
        return "$from $via $to"
    }
}

data class State(val id: Int, val isStartState: Boolean = false, val isAcceptingState: Boolean = false) {
    override fun toString(): String {
        return id.toString()
    }

    override fun equals(other: Any?): Boolean {
        return if(other is State) id == other.id else if (other is Number) id == other else false
    }
}

abstract class Automata(
        val cardinality: Int,
        val validTokens: String,
        val acceptingStates: Array<Int>,
        val startState: Int,
        val transitionMap: List<Transition>
){
    open fun repr(): String {
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