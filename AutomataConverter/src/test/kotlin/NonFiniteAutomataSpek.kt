package AutomataConverter.Tests

import AutomataConverter.toNonFiniteAutomata
import org.amshove.kluent.*
import org.jetbrains.spek.api.Spek
import org.jetbrains.spek.api.dsl.*

object NonFiniteAutomataSpek: Spek({
    describe("Non-Finite Automata") {
        on("Constructing a sample automata via string") {
            val nfaSource = """
3
ab
1
2
0
0 a 0
0 b 0
0 a 1
1 b 2
""".trim()
            val nfa = nfaSource.toNonFiniteAutomata()

            it("Detects the correct cardinality") {
                nfa.cardinality shouldBe 3
            }

            it("Detects the set of valid tokens") {
                nfa.validTokens shouldEqual "ab"
            }

            it("Detects the correct set of accepting states") {
                nfa.acceptingStates.size shouldBe 1
                nfa.acceptingStates[0] shouldBe 2
            }

            it("Detects the correct initial state") {
                nfa.startState shouldBe 0
            }

            it("Detects all transitions") {
                nfa.transitionMap.size shouldBe 4

                nfa.transitionMap[0].from shouldBe 0
                nfa.transitionMap[0].via  shouldBe 'a'
                nfa.transitionMap[0].to   shouldBe 0

                nfa.transitionMap[1].from shouldBe 0
                nfa.transitionMap[1].via  shouldBe 'b'
                nfa.transitionMap[1].to   shouldBe 0

                nfa.transitionMap[2].from shouldBe 0
                nfa.transitionMap[2].via  shouldBe 'a'
                nfa.transitionMap[2].to   shouldBe 1

                nfa.transitionMap[3].from shouldBe 1
                nfa.transitionMap[3].via  shouldBe 'b'
                nfa.transitionMap[3].to   shouldBe 2
            }

            it("Prints the correct representation") {
                nfa.repr() shouldEqual nfaSource
            }
        }
    }
})