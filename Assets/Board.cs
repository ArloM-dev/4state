using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public int gridsize = 2;
    public Material material;
    
    // declairing all game pieces
    public GameObject piece0, piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8, piece9, piece10, piece11, piece12, piece13, piece14, piece15;

    // Start is called before the first frame update
    void Start()
    {
        int[] grid = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        BuildGrid();
        DisplayGrid(grid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildGrid()
    {
        //creating the grid square mesh
        Vector3[] vertices = new Vector3[4];
        Vector2[] UV = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0,0);
        vertices[1] = new Vector3(0,2);
        vertices[2] = new Vector3(2,0);
        vertices[3] = new Vector3(2,2);

        UV[0] = new Vector2(0,0);
        UV[1] = new Vector2(0,2);
        UV[2] = new Vector2(2,0);
        UV[3] = new Vector2(2,2);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        Mesh gridmesh = new Mesh
        {
            vertices = vertices,
            uv = UV,
            triangles = triangles
        };

        GameObject[] gridsquares = new GameObject[16];
        for (int gridnum = 0; gridnum < 16; gridnum++)
        {
            GameObject currentsquare = gridsquares[gridnum];
            currentsquare = new GameObject("gridsquare"+gridnum, typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider2D), typeof(gridsquare));
            currentsquare.GetComponent<BoxCollider2D>().offset = new Vector2(1,1);
            currentsquare.GetComponent<BoxCollider2D>().size = new Vector2(2,2);
            currentsquare.GetComponent<MeshFilter>().mesh = gridmesh;
            currentsquare.GetComponent<MeshRenderer>().material = material;
            float x = (float)(2 * (gridnum % 4 - 2));
            float y = (float)(2 * (1 - ((gridnum - gridnum % 4) / 4)));
            currentsquare.transform.position = new Vector3(x,y);
        }
    }

    public bool WinDetect(int[] grid)
    {
        for (int cycle = 0; cycle < 4; cycle++)
        {
            //creating the custom quality grid
            bool[] sgrid = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                if ((grid[i] & (int)Math.Pow(2 , cycle)) == Math.Pow(2 , cycle))
                {
                    sgrid[i] = true;
                }
                else {sgrid[i] = false;}
            }
            //row win check
            for (int row = 0; row < 4; row+=4)
            {
                if (sgrid[row] && sgrid[row+1] && sgrid[row+2] && sgrid[row+3])
                {
                    return true;
                }
            }
            //column win check
            for (int column = 0; column < 4; column++)
            {
                if (sgrid[column] && sgrid[column+4] && sgrid[column+8] && sgrid[column+12])
                {
                    return true;
                }
            }
            //top to bottom diagonal win check
            if (sgrid[0] && sgrid[5] && sgrid[10] && sgrid[15])
            {
                return true;
            }
            //bottom to top diagonal win check
            if (sgrid[3] && sgrid[6] && sgrid[9] && sgrid[12])
            {
                return true;
            }
        }
        return false;
    }

    public void DisplayGrid(int[] grid)
    {
        GameObject[] piece = {piece0, piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8, piece9, piece10, piece11, piece12, piece13, piece14, piece15};
        
        //sets pieces to grid
        for (int pos = 0; pos < 16; pos++)
        {
            if (grid[pos] != 16)
            {
                float x = (float)(gridsize * (pos % 4 - 1.5));
                float y = (float)(gridsize * (1.5 - ((pos - pos % 4) / 4)));
                piece[grid[pos]].SetActive(true);
                piece[grid[pos]].transform.position = new Vector3(x, y, -1);
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
