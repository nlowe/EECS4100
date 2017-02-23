using Xunit;
using System.Linq;

namespace AutomataConverter.Tests
{
    public class NonFiniteAutomataFixture
    {
        public readonly NonFiniteAutomata NFA;
        public readonly string Source = @"
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

        public NonFiniteAutomataFixture()
        {
            NFA = NonFiniteAutomata.parse(Source);
        }
    }

    public class NonFiniteAutomataTests : IClassFixture<NonFiniteAutomataFixture>
    {
        private readonly NonFiniteAutomataFixture sut;

        public NonFiniteAutomataTests(NonFiniteAutomataFixture fixture)
        {
            sut = fixture;
        }

        [Fact]
        public void DetectsCorrectCardinality()
        {
            Assert.Equal(3, sut.NFA.Cardinality);
        }

        [Fact]
        public void DetectsValidTokens()
        {
            Assert.Equal("ab", sut.NFA.ValidTokens);
        }

        [Fact]
        public void DetectsCorrectAcceptingStates()
        {
            Assert.Equal(1, sut.NFA.AcceptingStates.Count());
            Assert.Equal(2, sut.NFA.AcceptingStates.First());
        }

        [Fact]
        public void PrintsCorrectRepresentation()
        {
            Assert.Equal(sut.Source, sut.NFA.ToString());
        }

        [Fact]
        public void CreatesCorrectTransitionMap()
        {
            var transitions = sut.NFA.TransitionMap.SelectMany(t => t.Value);

            Assert.Equal(4, transitions.Count());
            
            Assert.Contains(new Transition(0, 'a', 0), transitions);
            Assert.Contains(new Transition(0, 'b', 0), transitions);
            Assert.Contains(new Transition(0, 'a', 1), transitions);
            Assert.Contains(new Transition(1, 'b', 2), transitions);
        }
    }
}