using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class Main : MonoBehaviour
{
    // declairing all game pieces
    public GameObject piece0, piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8, piece9, piece10, piece11, piece12, piece13, piece14, piece15;

    // Start is called before the first frame update
    void Start()
    {
        MainGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MainGame()
    {
        int[] grid = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        DisplayGrid(grid);
    }

    public bool WinDetect(int[] grid)
    {
        for (int cycle = 0; cycle < 4; cycle++)
        {
            //creating the custom quality grid
            bool[] qgrid = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                if ((grid[i] & (int)Math.Pow(2 , cycle)) == Math.Pow(2 , cycle))
                {
                    qgrid[i] = true;
                }
                else {qgrid[i] = false;}
            }

            //horizontal win check
            for (int rowv = 0; rowv < 16; rowv+=4)
            {
                if (qgrid[rowv] && qgrid[rowv+1] && qgrid[rowv+2] && qgrid[rowv+3])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DisplayGrid(int[] grid)
    {
        GameObject[] piece = { piece0, piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8, piece9, piece10, piece11, piece12, piece13, piece14, piece15 };
        int gridsize = 2;
        //sets pieces to grid
        for (int pos = 0; pos < 16; pos++)
        {
            if (grid[pos] != 16)
            {
                float x = (float)(gridsize * (pos % 4 - 1.5));
                float y = (float)(gridsize * (1.5 - ((pos - pos % 4) / 4)));
                piece[grid[pos]].SetActive(true);
                piece[grid[pos]].transform.position = new Vector3(x, y);
            }
            //finds unused pieces and hides them
            for (int i = 0; i < 16; i++)
            {
                bool incol = false;
                foreach (int s in grid)
                {
                    if (i == s)
                    {
                        incol = true;
                        break;
                    }
                }
                if (incol == false)
                {
                    piece[i].SetActive(false);
                }
            }
        }
    }

}
