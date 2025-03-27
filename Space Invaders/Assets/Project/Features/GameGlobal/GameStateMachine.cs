using UnityEngine;

namespace Game.GameGlobal
{
    using SM;
    using Intro;
    using Menu;
    using Gameplay;

    public class GameStateMachine : MonoBehaviour
    {
        private readonly StateMachine _sm = new();

        private void Awake()
        {
            //Get game components
            Intro intro = FindFirstObjectByType<Intro>(FindObjectsInactive.Include);

            //Set transitions
            _sm.AddTransition(intro, GameInstance.Menu, new PredicateFunc(() => intro.Complete));
            _sm.AddTransition(GameInstance.Menu, GameInstance.Gameplay, new PredicateFunc(() => GameInstance.Menu.GameStarted));
            _sm.AddTransition(GameInstance.Gameplay, GameInstance.GameEnd, new PredicateFunc(() => GameInstance.Gameplay.GameEnded));
            _sm.AddTransition(GameInstance.GameEnd, GameInstance.Gameplay, new PredicateFunc(() => GameInstance.GameEnd.GameStarted));

            //Start intro
            _sm.SetState(intro);
        }

        private void Update()
        {
            _sm.Update();
        }
    }
}