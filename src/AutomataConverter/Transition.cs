namespace AutomataConverter
{
    public class Transition
    {
        public readonly int From;
        public readonly char Via;
        public readonly int To;

        public Transition(int from, char via, int to)
        {
            From = from;
            Via = via;
            To = to;
        }

        public override string ToString()
        {
            return $"{From} {Via} {To}";
        }

        public override bool Equals(object other)
        {
            if(other is Transition)
            {
                var t = other as Transition;

                return From == t.From && Via == t.Via && To == t.To;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() ^ Via.GetHashCode() ^ To.GetHashCode();
        }
    }
}
