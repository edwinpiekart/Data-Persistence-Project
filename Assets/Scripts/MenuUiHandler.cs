using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameField;

    void OnEnable()
    {
        ScoreManager.OnChangePlayer += ChangePlayerListener;
    }
    
    void OnDisable()
    {
        ScoreManager.OnChangePlayer -= ChangePlayerListener;
    }
    
    void Start()
    {
        ScoreManager.Instance.LoadPlayers();
    }
    
    public void StartGame()
    {
        string playerName = new string(PlayerNameField.text.Take(10).ToArray());
        ScoreManager.Instance.AddPlayer(playerName);
        ScoreManager.PlayerScore actualPlayer = ScoreManager.Instance.ActualPlayer;
        ScoreManager.Instance.SetBestScoreFromList();
        
        if (!String.IsNullOrEmpty(actualPlayer.Name))
        {
            ScoreManager.Instance.SavePlayer();
            SceneManager.LoadScene(1);
        }
    }

    void ChangePlayerListener()
    {
        PlayerNameField.text = ScoreManager.Instance.ActualPlayer.Name;
    }
}
