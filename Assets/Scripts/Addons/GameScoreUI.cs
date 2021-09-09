using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameScoreUI : MonoBehaviour
{
    public GameManager gameManager;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (gameManager)
        {
            text.text = gameManager.gameScore.ToString();
        }
    }
}
