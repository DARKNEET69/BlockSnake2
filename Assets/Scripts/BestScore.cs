using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    public TMP_Text Score;

    void Start()
    {
        if (!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", 0);        
    }

    private void OnEnable()
    {
        Score.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
}
