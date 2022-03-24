using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int bestScore;
    private string actualPlayer;

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

    public string ActualPlayer { get; private set; }

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

    void Start()
    {
        // PlayerScores.Add(new PlayerScore("Wałecki", 1));
        // PlayerScores.Add(new PlayerScore("Pałpecki", 2));

        // LogPlayers();
    }

    /*
     * TODO: do zrobienia - wersja bez serializacji i zapisywania na dysku:
     * 1. Przycisk Start zadziała tylko wówczas, gdy istnieje na liście jakiś gracz.
     * 2. Jeśli gracza na liście nie ma, to czeka na wpisanie.
     * 3. Na liście ostatni gracz jest tym aktualnym.
     */

    public void AddPlayer(string playerName)
    {
        playerName = String.IsNullOrEmpty(playerName) ? "Noname" : playerName;
        
        PlayerScore playerScore = PlayerScores.Find(
            (ps) => ps.Name == playerName
        );

        if (playerScore.Name == null)
            playerScore.Name = playerName;

        PlayerScores.Add(playerScore);
        ActualPlayer = playerScore.Name;

        LogPlayers();
    }

    private void LogPlayers()
    {
        foreach (PlayerScore ps in PlayerScores)
        {
            Debug.Log($"name: {ps.Name}; score: {ps.Score}");
        }
    }
}