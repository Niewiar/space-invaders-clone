using UnityEngine;

namespace Game.GameGlobal
{
    using SM;
    using Intro;

    public class GameStateMachine : MonoBehaviour
    {
        private readonly StateMachine _sm = new();

        private void Awake()
        {
            Intro intro = FindFirstObjectByType<Intro>();
            MainMenu mainMenu = new();

            _sm.AddTransition(intro, mainMenu, new PredicateFunc(() => intro.Complete));

            _sm.SetState(intro);
        }

        private void Update()
        {
            _sm.Update();
        }
    }
}