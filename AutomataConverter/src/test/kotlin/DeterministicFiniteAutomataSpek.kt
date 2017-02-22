package AutomataConverter.Tests

import AutomataConverter.State
import AutomataConverter.DeterministicFiniteAutomata
import AutomataConverter.NonFiniteAutomata
import AutomataConverter.Transition
import org.amshove.kluent.*
import org.jetbrains.spek.api.Spek
import org.jetbrains.spek.api.dsl.*

object DeterministicFiniteAutomataSpek: Spek({
    describe("Deterministic Finite Automata") {
        given("A valid nfa") {
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
            val dfa = DeterministicFiniteAutomata.fromNFA(nfa)

            val expectedDfa = """
3
ab
1
2
0
0 a 1
0 b 0
1 a 1
1 b 2
2 a 1
2 b 0
""".trim()

            it("Produces the correct cardinality") {
                dfa.cardinality shouldBe 3
            }

            it("Accepts the same set of tokens") {
                dfa.validTokens shouldEqual nfa.validTokens
            }

            it("Detects the correct set of accepting states") {
                dfa.acceptingStates.size shouldBe 1
                dfa.acceptingStates[0] shouldBe 2
            }

            it("The starting state is the first state") {
                dfa.startState shouldBe 0
            }

            describe("the transition map") {
                it("contains exactly 6 transitions") {
                    dfa.transitionMap.size shouldBe 6
                }

                it("contains a transition from 0 to 1 via a"){
                    dfa.transitionMap shouldContain Transition(State(0), 'a', State(1))
                }

                it("contains a transition from 0 to 0 via b"){
                    dfa.transitionMap shouldContain Transition(State(0), 'b', State(0))
                }

                it("contains a transition from 1 to 1 via a"){
                    dfa.transitionMap shouldContain Transition(State(1), 'a', State(1))
                }

                it("contains a transition from 1 to 2 via b"){
                    dfa.transitionMap shouldContain Transition(State(1), 'b', State(2))
                }

                it("contains a transition from 2 to 1 via a"){
                    dfa.transitionMap shouldContain Transition(State(2), 'a', State(1))
                }

                it("contains a transition from 2 to 0 via b"){
                    dfa.transitionMap shouldContain Transition(State(2), 'b', State(0))
                }
            }

            it("Prints the correct representation") {
                dfa.repr() shouldEqual expectedDfa
            }
        }

        /**
         * Models https://quickgrid.wordpress.com/2015/10/30/converting-nfa-to-dfa-by-complete-and-lazy-subset-construction/
         */
        given("a more complex nfa") {
            val nfaSource = """
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
""".trim()
            val nfa = NonFiniteAutomata.parse(nfaSource)
            val dfa = DeterministicFiniteAutomata.fromNFA(nfa)

            it("Produces the correct cardinality") {
                dfa.cardinality shouldBe 8
            }

            it("Accepts the same set of tokens") {
                dfa.validTokens shouldEqual nfa.validTokens
            }

            it("Detects the correct set of accepting states") {
                dfa.acceptingStates.size shouldBe 4
                dfa.acceptingStates shouldContain 3
                dfa.acceptingStates shouldContain 4
                dfa.acceptingStates shouldContain 5
                dfa.acceptingStates shouldContain 6
            }

            it("The starting state is the first state") {
                dfa.startState shouldBe 0
            }

            describe("the transition map") {
                it("contains exactly 6 transitions") {
                    dfa.transitionMap.size shouldBe 16
                }

                it("contains a transition from 0 to 1 via a"){
                    dfa.transitionMap shouldContain Transition(State(0), 'a', State(1))
                }

                it("contains a transition from 0 to 0 via b"){
                    dfa.transitionMap shouldContain Transition(State(0), 'b', State(0))
                }

                it("contains a transition from 1 to 7 via a"){
                    dfa.transitionMap shouldContain Transition(State(1), 'a', State(7))
                }

                it("contains a transition from 1 to 2 via b"){
                    dfa.transitionMap shouldContain Transition(State(1), 'b', State(2))
                }

                it("contains a transition from 2 to 3 via a"){
                    dfa.transitionMap shouldContain Transition(State(2), 'a', State(3))
                }

                it("contains a transition from 2 to 0 via b"){
                    dfa.transitionMap shouldContain Transition(State(2), 'b', State(0))
                }

                it("contains a transition from 3 to 6 via a"){
                    dfa.transitionMap shouldContain Transition(State(3), 'a', State(6))
                }

                it("contains a transition from 3 to 4 via b"){
                    dfa.transitionMap shouldContain Transition(State(3), 'b', State(4))
                }

                it("contains a transition from 4 to 3 via a"){
                    dfa.transitionMap shouldContain Transition(State(4), 'a', State(3))
                }

                it("contains a transition from 4 to 5 via b"){
                    dfa.transitionMap shouldContain Transition(State(4), 'b', State(5))
                }

                it("contains a transition from 5 to 3 via a"){
                    dfa.transitionMap shouldContain Transition(State(5), 'a', State(3))
                }

                it("contains a transition from 5 to 5 via b"){
                    dfa.transitionMap shouldContain Transition(State(5), 'b', State(5))
                }

                it("contains a transition from 6 to 6 via a"){
                    dfa.transitionMap shouldContain Transition(State(6), 'a', State(6))
                }

                it("contains a transition from 6 to 4 via b"){
                    dfa.transitionMap shouldContain Transition(State(6), 'b', State(4))
                }

                it("contains a transition from 7 to 6 via a") {
                    dfa.transitionMap shouldContain Transition(State(7), 'a', State(6))
                }

                it("contains a transition from 7 to 2 via b") {
                    dfa.transitionMap shouldContain Transition(State(7), 'b', State(2))
                }
            }
        }
    }
})