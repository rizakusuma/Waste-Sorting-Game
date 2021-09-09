using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLifeUI : MonoBehaviour
{
    public GameManager gameManager;
    public Image[] lifes;

    private void Update()
    {
        if (gameManager)
        {
            for (int i = 0; i < lifes.Length; i++)
            {
                int ind = i;
                if (ind < gameManager.remainingLife)
                {
                    lifes[ind].color = new Color(1, 1, 1, 0.5f);
                } else
                {
                    lifes[ind].color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}
