using System;
using UnityEngine;
using System.IO;

namespace Game.GameGlobal
{
    using Menu;
    using Gameplay;

    public static class GameInstance
    {
        public static GameplayController Gameplay;
        public static Menu Menu;
        public static GameConfig Config;

        private static string CONFIG_PATH => Path.Combine(Application.persistentDataPath, "GameConfig.json");

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Menu = UnityEngine.Object.FindFirstObjectByType<Menu>(FindObjectsInactive.Include);
            Gameplay = UnityEngine.Object.FindFirstObjectByType<GameplayController>(FindObjectsInactive.Include);

            LoadConfig();

            GameObject gameObj = new GameObject("#Game Instance#");

            gameObj.AddComponent<GameStateMachine>();

            UnityEngine.Object.DontDestroyOnLoad(gameObj);
        }

        private static void LoadConfig()
        {
            if (File.Exists(CONFIG_PATH))
            {
                string json = File.ReadAllText(CONFIG_PATH);
                Config = JsonUtility.FromJson<GameConfig>(json);
                Debug.Log("Config loaded: " + CONFIG_PATH);
            }
            else
            {
                Debug.Log("Config not found. Create new: " + CONFIG_PATH);
                Config = new GameConfig();

                Config.PlayerStartLives = 3;

                Config.PlayerSpeed = 3;

                Config.StartEnemiesRowSpeed = 1f;
                Config.EnemiesRowSpeedupValue = .2f;
                Config.DestroyedEnemiesAmoutToSpeedup = 10;

                Config.BulletSpeed = 6;

                Config.EnemiesConfigs = new()
                {
                    new EnemieConfig() { 
                        KillPoints = 10, 
                        Sprite1 = "Enemy0_1.png", 
                        Sprite1SizeX = 9, Sprite1SizeY = 8,
                        Sprite2 = "Enemy0_2.png",
                        Sprite2SizeX = 9, Sprite2SizeY = 8,
                    },
                    new EnemieConfig() { 
                        KillPoints = 20, 
                        Sprite1 = "Enemy1_1.png",
                        Sprite1SizeX = 11, Sprite1SizeY = 8,
                        Sprite2 = "Enemy1_2.png",
                        Sprite2SizeX = 11, Sprite2SizeY = 9
                    },
                    new EnemieConfig() { 
                        KillPoints = 30, 
                        Sprite1 = "Enemy2_1.png",
                        Sprite1SizeX = 8, Sprite1SizeY = 8,
                        Sprite2 = "Enemy2_2.png",
                        Sprite2SizeX = 8, Sprite2SizeY = 8,
                    }
                };

                Config.EnemieRowsConfigs = new()
                {
                    new EnemieRowConfig() { EnemieIndex = 0 },
                    new EnemieRowConfig() { EnemieIndex = 0 },
                    new EnemieRowConfig() { EnemieIndex = 1 },
                    new EnemieRowConfig() { EnemieIndex = 1 },
                    new EnemieRowConfig() { EnemieIndex = 2 }

                };

                string json = JsonUtility.ToJson(Config, true);
                File.WriteAllText(CONFIG_PATH, json);
            }
        }
    }
}