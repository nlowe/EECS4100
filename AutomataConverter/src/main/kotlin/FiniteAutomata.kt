package AutomataConverter

data class Transition(val from: Int, val via: Char, val to: Int) {
    override fun toString(): String {
        return "$from $via $to"
    }
}

abstract class FiniteAutomata(
        val cardinality: Int,
        val validTokens: String,
        val acceptingStates: Array<Int>,
        val startState: Int,
        val transitionMap: List<Transition>
){
    abstract fun repr(): String
}