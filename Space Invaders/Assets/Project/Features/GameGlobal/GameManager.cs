using UnityEngine;

namespace Game.GameGlobal
{
    public static class GameInstance
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            GameObject gameObj = new GameObject("#Game Instance#");

            gameObj.AddComponent<GameStateMachine>();

            Object.DontDestroyOnLoad(gameObj);
        }
    }
}