using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameField;
    private TMP_Dropdown dropdown;

    void OnEnable()
    {
        ScoreManager.OnChangePlayer += ChangePlayerListener;
    }
    
    void OnDisable()
    {
        ScoreManager.OnChangePlayer -= ChangePlayerListener;
    }

    void Awake()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
    }
    
    void Start()
    {
        dropdown.options.Clear();

        foreach (ScoreManager.PlayerScore playerScore in ScoreManager.Instance.PlayerScores)
        {
            dropdown.options.Add(
                new TMP_Dropdown.OptionData() {text = playerScore.Name}
            );
        }
        
        DropdownItemSelected();
        dropdown.onValueChanged.AddListener(
            delegate { DropdownItemSelected(); }
        );
    }

    void SetDropdownValue(string newOption)
    {
        var listAvailableStrings = dropdown.options.Select(option => option.text).ToList();
        dropdown.value = listAvailableStrings.IndexOf(newOption);
    }

    void DropdownItemSelected()
    {
        int index = dropdown.value;
        
        if (index >= 0 && dropdown.options.Count > 0)
        {
            PlayerNameField.text = dropdown.options[index].text;
        }
    }

    void ChangePlayerListener()
    {
        ScoreManager.PlayerScore actualPlayer = ScoreManager.Instance.ActualPlayer;
        StartCoroutine(ChangeDropdownValue(actualPlayer.Name));
    }

    IEnumerator ChangeDropdownValue(string actualPlayer)
    {
        yield return new WaitForSeconds(0.3f);
        SetDropdownValue(actualPlayer);
    }
}