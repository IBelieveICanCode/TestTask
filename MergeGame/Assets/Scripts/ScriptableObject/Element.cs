using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Elements", menuName = "NewElement", order = 52)]
public class Element : ScriptableObject
{
    public List<ElementLevel> ElementLevels;

}


[System.Serializable]
public class ElementLevel
{
    public string ElementName;
    [SerializeField]
    private int scoreForLeveling;
    public int ScoreForLeveling => scoreForLeveling;
    [SerializeField]
    private Material itemMaterial;
    public Material ItemMaterial => itemMaterial;
}