using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameTimeLeftUI : MonoBehaviour
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
            int minute = (int)gameManager.remainingTime / 60;
            int second = (int)gameManager.remainingTime % 60;
            text.text = $"{minute}:{second.ToString("00")}";
        }
    }
}
