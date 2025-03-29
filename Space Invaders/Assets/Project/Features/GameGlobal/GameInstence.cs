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
        public static GameEnd GameEnd;

        private static string CONFIG_PATH => Path.Combine(Application.persistentDataPath, "GameConfig.json");

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Menu = UnityEngine.Object.FindFirstObjectByType<Menu>(FindObjectsInactive.Include);
            Gameplay = UnityEngine.Object.FindFirstObjectByType<GameplayController>(FindObjectsInactive.Include);
            GameEnd = UnityEngine.Object.FindFirstObjectByType<GameEnd>(FindObjectsInactive.Include);

            Config = LoadConfig();

            if (!PlayerPrefs.HasKey("Record"))
            {
                PlayerPrefs.SetInt("Record", 0);
            }

            GameObject gameObj = new GameObject("#Game Instance#");

            gameObj.AddComponent<GameStateMachine>();

            UnityEngine.Object.DontDestroyOnLoad(gameObj);
        }

        public static GameConfig LoadConfig()
        {
            GameConfig config;

            if (File.Exists(CONFIG_PATH))
            {
                string json = File.ReadAllText(CONFIG_PATH);
                config = JsonUtility.FromJson<GameConfig>(json);
                return config;
            }
            else
            {
                Debug.Log("Config not found. Create new: " + CONFIG_PATH);
                config = new GameConfig();

                config.PlayerStartLives = 3;

                config.PlayerSpeed = 3;

                config.StartEnemiesRowSpeed = 1f;
                config.EnemiesRowSpeedupValue = .2f;
                config.DestroyedEnemiesAmoutToSpeedup = 10;

                config.UfoMinPoints = 100;
                config.UfoMaxPoints = 300;
                config.UfoMinShowTime = 60;
                config.UfoMaxShowTime = 120;
                config.UfoSpeed = 3;
                
                config.BulletSpeed = 6;

                config.PointsToGetExtraLive = 1000;
                
                config.EnemiesConfigs = new()
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

                config.EnemieRowsConfigs = new()
                {
                    new EnemieRowConfig() { EnemieIndex = 0 },
                    new EnemieRowConfig() { EnemieIndex = 0 },
                    new EnemieRowConfig() { EnemieIndex = 1 },
                    new EnemieRowConfig() { EnemieIndex = 1 },
                    new EnemieRowConfig() { EnemieIndex = 2 }

                };

                string json = JsonUtility.ToJson(config, true);
                File.WriteAllText(CONFIG_PATH, json);
            }

            return config;
        }
    }
}