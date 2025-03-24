namespace Game.SM
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}
