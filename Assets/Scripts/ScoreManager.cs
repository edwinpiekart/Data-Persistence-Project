using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int bestScore;

    public int BestScore
    {
        get => bestScore;
        set
        {
            if (value > bestScore)
            {
                bestScore = value;
                Debug.Log($"bestScore: {bestScore}");
            }
        }
    }

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
}