using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Game.GameGlobal
{
    [Serializable]
    public class GameConfig
    {
        public float PlayerSpeed;
        public int PlayerStartLives;

        public float StartEnemiesRowSpeed;
        public float EnemiesRowSpeedupValue;
        public int DestroyedEnemiesAmoutToSpeedup;

        public int UfoMinPoints;
        public int UfoMaxPoints;
        public float UfoMinShowTime;
        public float UfoMaxShowTime;
        public float UfoSpeed;

        public float BulletSpeed;

        public List<EnemieConfig> EnemiesConfigs;
        public List<EnemieRowConfig> EnemieRowsConfigs;

        public LoadedEnemieData GetEnemiesRowData(int p_rowIndex)
        {
            EnemieConfig enemie = EnemiesConfigs[EnemieRowsConfigs[p_rowIndex].EnemieIndex];
            LoadedEnemieData data = new LoadedEnemieData();
            data.Sprite1 = LoadSprite(enemie.Sprite1, new(enemie.Sprite1SizeX, enemie.Sprite1SizeY));
            data.Sprite2 = LoadSprite(enemie.Sprite2, new(enemie.Sprite2SizeX, enemie.Sprite2SizeY));
            data.Points = enemie.KillPoints;
            return data;
        }

        private Sprite LoadSprite(string p_fileName, Vector2Int p_size)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, p_fileName);
            if (!File.Exists(filePath))
            {
                Debug.LogError("File doesn't exist: " + filePath);
                return null;
            }

            try
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(p_size.x, p_size.y);
                if (texture.LoadImage(fileData))
                {
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Read file error: " + ex.Message);
            }

            return null;
        }
    }

    public class LoadedEnemieData
    {
        public Sprite Sprite1;
        public Sprite Sprite2;
        public int Points;
    }

    [Serializable]
    public class EnemieRowConfig
    {
        public int EnemieIndex;
    }

    [Serializable]
    public class EnemieConfig
    {
        public string Sprite1;
        public int Sprite1SizeX;
        public int Sprite1SizeY;
        public string Sprite2;
        public int Sprite2SizeX;
        public int Sprite2SizeY;
        public int KillPoints;
    }
}
