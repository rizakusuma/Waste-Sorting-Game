using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHighscoreUI : MonoBehaviour
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
            text.text = $"Score: {gameManager.gameScore}\nBest: {gameManager.lastHighscore}";
        }
    }
}
