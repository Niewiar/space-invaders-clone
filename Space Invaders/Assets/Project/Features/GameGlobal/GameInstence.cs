using System;
using UnityEngine;

namespace Game.GameGlobal
{
    using Menu;
    using Gameplay;

    public static class GameInstance
    {
        public static GameplayController Gameplay;
        public static Menu Menu;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Menu = UnityEngine.Object.FindFirstObjectByType<Menu>(FindObjectsInactive.Include);
            Gameplay = UnityEngine.Object.FindFirstObjectByType<GameplayController>(FindObjectsInactive.Include);

            GameObject gameObj = new GameObject("#Game Instance#");

            gameObj.AddComponent<GameStateMachine>();

            UnityEngine.Object.DontDestroyOnLoad(gameObj);
        }
    }
}