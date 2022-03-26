using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int bestScore;
    private string actualPlayer;
    // Events
    public delegate void ChangePlayerAction();

    public static event ChangePlayerAction OnChangePlayer;
    

    public int BestScore
    {
        get => bestScore;
        set
        {
            if (value > bestScore)
            {
                bestScore = value;
            }
        }
    }

    public string ActualPlayer
    {
        get => actualPlayer;

        private set
        {
            actualPlayer = value;
            OnChangePlayer?.Invoke();
        }
    }

    [Serializable]
    public struct PlayerScore
    {
        public string Name;
        public int Score;

        public PlayerScore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }

    public List<PlayerScore> PlayerScores = new List<PlayerScore>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(string playerName)
    {
        playerName = String.IsNullOrEmpty(playerName) ? "Noname" : playerName;
        
        PlayerScore playerScore = PlayerScores.Find(
            (ps) => ps.Name == playerName
        );

        if (playerScore.Name == null)
        {
            playerScore.Name = playerName;
            PlayerScores.Add(playerScore);
        }

        ActualPlayer = playerScore.Name;
    }
    
    [System.Serializable]
    class SaveData
    {
        public string Player;
        public List<PlayerScore> SavedPlayerScores;
    }

    public void SavePlayer()
    {
        SaveData saveData = new SaveData
        {
            Player = ActualPlayer,
            SavedPlayerScores = PlayerScores
        };
        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/player_data.json", json);
    }

    public void LoadPlayers()
    {
        string path = Application.persistentDataPath + "/player_data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            ActualPlayer = saveData.Player;
            PlayerScores = saveData.SavedPlayerScores;
        }
    }
}