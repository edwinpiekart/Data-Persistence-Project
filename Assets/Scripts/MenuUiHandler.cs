using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameField;

    void Start()
    {
        ScoreManager.Instance.LoadPlayers();
    }
    
    public void StartGame()
    {
        ScoreManager.Instance.AddPlayer(PlayerNameField.text);
        string actualPlayer = ScoreManager.Instance.ActualPlayer;
        
        if (!String.IsNullOrEmpty(actualPlayer))
        {
            ScoreManager.Instance.SavePlayer();
            SceneManager.LoadScene(1);
        }
    }
}
