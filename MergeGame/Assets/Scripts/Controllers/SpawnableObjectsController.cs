using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjectsController : MonoBehaviour
{
    [SerializeField]
    public List<Element> SpawnableElements;

    [SerializeField]
    private Item basicItem;
    public Item BasicItem => basicItem;

    public Item SpawnElement(int indexOfElement, Coord coordToSpawn, int level)
    { 
        Element _elem = SpawnableElements[indexOfElement];
        if (level - 1 < _elem.ElementLevels.Count) //Check if destroyed object was at max level
        {
            Vector3 spawnCoord = new Vector3(coordToSpawn.x, coordToSpawn.y, -0.1f);

            Item _basicItem = Instantiate(BasicItem, spawnCoord, Quaternion.identity);
            _basicItem.GetComponent<Renderer>().material = _elem.ElementLevels[level - 1].ItemMaterial;
            _basicItem.name = _elem.ElementLevels[level - 1].ElementName;
            _basicItem.Level = level;
            _basicItem.Score = _elem.ElementLevels[level - 1].ScoreForLeveling;
            _basicItem.Index = indexOfElement;
            return _basicItem;
        }
        else // and if yes - return nothing
            return null;
    }

    public void UpgradeElement(Item item1, Item item2)
    {
        Coord _coord = new Coord(item2.transform.position.x, item2.transform.position.y);
        int _level = item2.GetComponent<Item>().Level;

        GameController.Instance.Score += item2.Score;

        Destroy(item1.gameObject);
        Destroy(item2.gameObject);        
        SpawnElement(item2.Index, _coord, _level + 1);
    }
}

