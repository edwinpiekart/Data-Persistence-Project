using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private PlayerScore bestScore;
    private PlayerScore actualPlayer;
    // Events
    public delegate void ChangePlayerAction();
    public static event ChangePlayerAction OnChangePlayer;
    
    public PlayerScore BestScore
    {
        get => bestScore;
        set
        {
            if (value.Score > bestScore.Score)
            {
                bestScore = value;
            }
        }
    }

    public PlayerScore ActualPlayer
    {
        get => actualPlayer;

        set
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

        ActualPlayer = playerScore;
    }
    
    [System.Serializable]
    class SaveData
    {
        public PlayerScore Player;
        public List<PlayerScore> SavedPlayerScores;
    }

    public void SavePlayer()
    {
        UpdatePlayersList();
        
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

    public void SetBestScoreFromList()
    {
        int bs = 0;
        // which one is the best?
        foreach (PlayerScore ps in PlayerScores)
        {
            if (ps.Score > bs)
                bs = ps.Score;
        }
        // bs has best score
        PlayerScore bScore = PlayerScores.Find(
            (ps2) => ps2.Score == bs
            );
        // bScore is the best :-)
        BestScore = bScore;
    }

    void UpdatePlayersList()
    {
        int i = PlayerScores.FindIndex(
            (ps) => ps.Name == ActualPlayer.Name
        );
        if (PlayerScores[i].Score < ActualPlayer.Score)
            PlayerScores[i] = ActualPlayer;
    }
}