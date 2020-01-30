using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    [SerializeField]
    private Text scoreText;

    public void SetScore(int _score)
    {
        scoreText.text = _score.ToString();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
