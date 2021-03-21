using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public FloatReference timeSpent;
    public TextMeshProUGUI gameOverText;

    private void Start()
    {
        gameOverText.text = "GAME OVER\nYOU LASTED " + ((int)timeSpent.Value).ToString() + " seconds";
    }

}
