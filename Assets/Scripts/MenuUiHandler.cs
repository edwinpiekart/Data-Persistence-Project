using System;
using System.Collections;
using System.Collections.Generic;
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
        ScoreManager.Instance.AddPlayer(PlayerNameField.text);
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
