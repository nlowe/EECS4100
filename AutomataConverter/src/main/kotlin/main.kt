package AutomataConverter;

fun main(args: Array<String>) {
    val nfa = NonFiniteAutomata.parse("""
4
ab
1
3
0
0 a 0
0 b 0
0 a 1
1 a 2
1 b 2
2 a 3
3 a 3
3 b 3
""".trim())

    val dfa = DeterministicFiniteAutomata.fromNFA(nfa)

    println("NFA:\n--------------")
    println(nfa.repr())

    println("\n\nDFA:\n----------------")
    println(dfa.repr())
}