using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{

    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void OnScoreChange(object data)
    {
        if(data is int && data != null)
        {
            scoreText.text = data.ToString();
        }
    }

}
