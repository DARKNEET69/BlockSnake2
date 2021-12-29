using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TMP_Text Score;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Score.text = Mathf.Round(GameObject.FindGameObjectWithTag("Player").transform.position.y / 10).ToString();
        }        
    }
}
