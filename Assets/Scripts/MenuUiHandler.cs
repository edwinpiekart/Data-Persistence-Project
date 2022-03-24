using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameField;
    public void StartGame()
    {
        AddPlayer();
        string actualPlayer = ScoreManager.Instance.ActualPlayer;
        Debug.Log($"actualPlayer: {actualPlayer}");
        if (!String.IsNullOrEmpty(actualPlayer))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void AddPlayer()
    {
        ScoreManager.Instance.AddPlayer(PlayerNameField.text);
    }
}
