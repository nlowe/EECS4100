using Xunit;
using System.Linq;

namespace AutomataConverter.Tests
{
    public class ComplexDFAFixture
    {
        public readonly NonFiniteAutomata NFA;
        public readonly DeterministicFiniteAutomata DFA;
        public readonly string NFASource = @"
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
".Trim();

        public ComplexDFAFixture()
        {
            NFA = NonFiniteAutomata.parse(NFASource);
            DFA = NFA.convertToDFA();
        }
    }

    public class ComplexDeterministicFiniteAutomataTests : IClassFixture<ComplexDFAFixture>
    {
        public readonly ComplexDFAFixture sut;

        public ComplexDeterministicFiniteAutomataTests(ComplexDFAFixture fixture)
        {
            sut = fixture;
        }

        [Fact]
        public void ProducesCorrectCardinality()
        {
            Assert.Equal(8, sut.DFA.Cardinality);
        }

        [Fact]
        public void AcceptsTheSameSetOfTokens()
        {
            Assert.Equal(sut.NFA.ValidTokens, sut.DFA.ValidTokens);
        }

        [Fact]
        public void DetectsCorrectAcceptingStates()
        {
            Assert.Equal(4, sut.DFA.AcceptingStates.Count());
            Assert.Contains(3, sut.DFA.AcceptingStates);
            Assert.Contains(4, sut.DFA.AcceptingStates);
            Assert.Contains(5, sut.DFA.AcceptingStates);
            Assert.Contains(6, sut.DFA.AcceptingStates);
        }

        [Fact]
        public void StartStateIsStateZero()
        {
            Assert.Equal(0, sut.DFA.StartState);
        }

        [Fact]
        public void BuildsCorrectTransitionTable()
        {
            var transitions = sut.DFA.TransitionMap.SelectMany(t => t.Value);

            Assert.Equal(16, transitions.Count());
            Assert.Contains(new Transition(0, 'a', 1), transitions);
            Assert.Contains(new Transition(0, 'b', 0), transitions);
            Assert.Contains(new Transition(1, 'a', 7), transitions);
            Assert.Contains(new Transition(1, 'b', 2), transitions);
            Assert.Contains(new Transition(2, 'a', 3), transitions);
            Assert.Contains(new Transition(2, 'b', 0), transitions);
            Assert.Contains(new Transition(3, 'a', 6), transitions);
            Assert.Contains(new Transition(3, 'b', 4), transitions);
            Assert.Contains(new Transition(4, 'a', 3), transitions);
            Assert.Contains(new Transition(4, 'b', 5), transitions);
            Assert.Contains(new Transition(5, 'a', 3), transitions);
            Assert.Contains(new Transition(5, 'b', 5), transitions);
            Assert.Contains(new Transition(6, 'a', 6), transitions);
            Assert.Contains(new Transition(6, 'b', 4), transitions);
            Assert.Contains(new Transition(7, 'a', 6), transitions);
            Assert.Contains(new Transition(7, 'b', 2), transitions);
        }
    }
}