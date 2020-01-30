using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private int score = 0;
    public int Score
    {
        get => score;
        set
        {
            score += value;
            HUD.Instance.SetScore(score);
        }
    }

    [SerializeField]
    private int levelSize = 5;

    [SerializeField]
    private PlayingField fieldClass;
    [SerializeField]
    public SpawnableObjectsController itemController;

    void Start ()
    {
        InitializeLevel();
	}

    private void InitializeLevel()
    {
        fieldClass.CreateField(levelSize);
        AddElement();
    }

    public void AddElement()
    {
        int whatElement = Random.Range(0, itemController.SpawnableElements.Count);
        itemController.SpawnElement(whatElement, fieldClass.GetRandomCoord(), 1);
    }
}
