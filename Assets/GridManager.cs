using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 10;
    [SerializeField]
    private int cols = 10;
    [SerializeField]
    private float tileSize = 1;
  
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Resources.Load("TilePrefab");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = j * tileSize;
                float posY = i * tileSize;

                tile.transform.position = new Vector3(posX, posY, 0);
            }
        }

        //float gridW = cols * tileSize;
        //float gridH = rows * tileSize;
        //transform.position = new Vector3(-gridW/2+tileSize/2, gridH/2-tileSize/2, 0);
    }

   
}
