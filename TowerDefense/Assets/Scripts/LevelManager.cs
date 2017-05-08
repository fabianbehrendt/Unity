using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform gameBoard;


    private List<Transform> tiles = new List<Transform>();


    private void Awake()
    {
        for (int i = 0; i < gameBoard.childCount; i++)
        {
            tiles.Add(gameBoard.GetChild(i));
        }

        foreach (Transform tile in tiles)
        {
            tile.GetComponent<Tile>().IsTaken = false;
        }
    }
}
