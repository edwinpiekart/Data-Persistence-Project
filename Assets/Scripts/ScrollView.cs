using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private GameObject contentRect;
    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private TMP_Text textPrefab;

    void OnEnable()
    {
        ScoreManager.OnLoadComplete += FillScrollList;
    }

    void OnDisable()
    {
        ScoreManager.OnLoadComplete -= FillScrollList;
    }

    void FillScrollList()
    {
        List<ScoreManager.PlayerScore> orderedList = ScoreManager
            .Instance
            .PlayerScores
            .OrderByDescending(ps => ps.Score)
            .ToList();
        
        foreach (ScoreManager.PlayerScore ps in orderedList)
        {
            GameObject listItem = Instantiate(listItemPrefab, contentRect.transform);
            TMP_Text listText = listItem.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            TMP_Text listScore = listItem.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

            listText.text = ps.Name;
            listScore.text = ps.Score.ToString();
        }
    }
}