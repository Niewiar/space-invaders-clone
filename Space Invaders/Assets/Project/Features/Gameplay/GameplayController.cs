using System;
using UnityEngine;

namespace Game.Gameplay
{
    using SM;

    public class GameplayController : MonoState
    {
        public event Action OnGameStarted;
        public event Action OnGameEnded;

        public override void OnEnter()
        {
            OnGameStarted?.Invoke();
        }

        public override void OnExit()
        {
            OnGameEnded?.Invoke();
        }
    }
}
