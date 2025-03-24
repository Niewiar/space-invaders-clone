namespace Game.SM
{
    public class Transition : ITransition
    {
        public IState TargetState { get; }

        public IPredicate Condition { get; }

        public Transition(IState p_to, IPredicate p_condition)
        {
            TargetState = p_to;
            Condition = p_condition;
        }
    }
}