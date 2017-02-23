using Xunit;
using System.Linq;

namespace AutomataConverter.Tests
{
    public class DFAFixture
    {
        public readonly NonFiniteAutomata NFA;
        public readonly DeterministicFiniteAutomata DFA;
        public readonly string NFASource = @"
3
ab
1
2
0
0 a 0
0 b 0
0 a 1
1 b 2
".Trim();

        public readonly string ExpectedDFA = @"
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
".Trim();

        public DFAFixture()
        {
            NFA = NonFiniteAutomata.parse(NFASource);
            DFA = NFA.convertToDFA();
        }
    }

    public class SimpleDeterministicFiniteAutomataTests : IClassFixture<DFAFixture>
    {
        public readonly DFAFixture sut;

        public SimpleDeterministicFiniteAutomataTests(DFAFixture fixture)
        {
            sut = fixture;
        }

        [Fact]
        public void ProducesCorrectCardinality()
        {
            Assert.Equal(3, sut.DFA.Cardinality);
        }

        [Fact]
        public void AcceptsTheSameSetOfTokens()
        {
            Assert.Equal(sut.NFA.ValidTokens, sut.DFA.ValidTokens);
        }

        [Fact]
        public void DetectsCorrectAcceptingStates()
        {
            Assert.Equal(1, sut.DFA.AcceptingStates.Count());
            Assert.Equal(2, sut.DFA.AcceptingStates.First());
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

            Assert.Equal(6, transitions.Count());
            Assert.Contains(new Transition(0, 'a', 1), transitions);
            Assert.Contains(new Transition(0, 'b', 0), transitions);
            Assert.Contains(new Transition(1, 'a', 1), transitions);
            Assert.Contains(new Transition(1, 'b', 2), transitions);
            Assert.Contains(new Transition(2, 'a', 1), transitions);
            Assert.Contains(new Transition(2, 'b', 0), transitions);
        }
    }
}