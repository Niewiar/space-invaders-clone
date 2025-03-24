namespace Game.SM
{
    public abstract class State : IState
    {
        public virtual void OnEnter(){}

        public virtual void OnUpdate(){}
        
        public virtual void OnExit(){}
    }
}
