using UnityEngine;

namespace Game.SM
{
    public abstract class MonoState : MonoBehaviour, IState
    {
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
    }
}
