using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField]
    private Transform tilePrefab;

    private int seed;

    private List<Coord> grid;
    private Queue<Coord> shuffledGrid;
    public Queue<Coord> ShuffledGrid => shuffledGrid;

    public void CreateField(int fieldSize)
    {
        if (fieldSize % 2 == 0)
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, 0);
        }

        GetComponent<BoxCollider>().size = new Vector3(fieldSize, fieldSize, 0.1f);
        grid = new List<Coord>();
        
        //Create Tiles and grid
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                Vector3 tilePosition = new Vector3(-fieldSize / 2 + x, -fieldSize / 2 + y, 1);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                newTile.parent = transform;

                grid.Add(new Coord(tilePosition.x, tilePosition.y));
            }
        }

        //Shuffle grid for picking up a random position for objects
        seed = Random.Range(0, int.MaxValue);
        shuffledGrid = new Queue<Coord>(Utility.ShuffleArray(grid.ToArray(), seed));

        //Camera set up
        Vector3 cameraPosition = transform.position;
        cameraPosition.z = -1;
        Camera.main.transform.position = cameraPosition;
        Camera.main.orthographicSize = (float)fieldSize * 0.7f;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledGrid.Dequeue();
        shuffledGrid.Enqueue(randomCoord);
        return randomCoord;
    }
}

[System.Serializable]
public struct Coord
{
    public float x;
    public float y;

    public Coord(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

}