package AutomataConverter.Tests

import AutomataConverter.NonFiniteAutomata
import AutomataConverter.State
import AutomataConverter.Transition
import org.amshove.kluent.*
import org.jetbrains.spek.api.Spek
import org.jetbrains.spek.api.dsl.*

object NonFiniteAutomataSpek: Spek({
    describe("Non-Finite Automata") {
        given("the source of a valid NFA") {
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
            val nfa = NonFiniteAutomata.parse(nfaSource)

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

            describe("the transition map"){
                it("contains exactly 4 transitions"){
                    nfa.transitionMap.size shouldBe 4
                }

                it("contains a transition from 0 to 0 via a"){
                    nfa.transitionMap shouldContain Transition(State(0), 'a', State(0))
                }

                it("contains a transition from 0 to 0 via b"){
                    nfa.transitionMap shouldContain Transition(State(0), 'b', State(0))
                }

                it("contains a transition from 0 to 1 via a"){
                    nfa.transitionMap shouldContain Transition(State(0), 'a', State(1))
                }

                it("contains a transition from 1 to 2 via b"){
                    nfa.transitionMap shouldContain Transition(State(1), 'b', State(2))
                }
            }

            it("Prints the correct representation") {
                nfa.repr() shouldEqual nfaSource
            }
        }

        given("The source of an NFA containing a transition via a string of more than one character") {
            val nfaSource = """
3
ab
1
2
0
0 abc 0
0 b 0
0 a 1
1 b 2
""".trim()

            it("Throws an IllegalStateException") {
                try {
                    NonFiniteAutomata.parse(nfaSource)
                    kotlin.test.fail("Exception was not thrown")
                }
                catch(e: Exception)
                {
                    e shouldBeInstanceOf IllegalStateException::class.java
                    e.message shouldEqual "Transition state must be composed of a single character (got abc)"
                }
            }
        }
    }
})