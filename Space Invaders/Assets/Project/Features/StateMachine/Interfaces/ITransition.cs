namespace Game.SM
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Condition { get; }
    }
}