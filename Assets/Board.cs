using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public GameObject[] pgridsquares = new GameObject[16];
    public GameObject[] pieces = new GameObject[16];
    public int[] grid = new int[16];
    public int gridsize = 2;
    public Material material;
    // declairing all game pieces
    public Sprite piece0s, piece1s, piece2s, piece3s, piece4s, piece5s, piece6s, piece7s;

    // Start is called before the first frame update
    public void Start()
    {
        pgridsquares = BuildGrid();
        pieces = Createpieces();
        setside();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] BuildGrid()
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
            gridsquares[gridnum] = new GameObject(gridnum.ToString(), typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider2D), typeof(gridsquare));
            gridsquares[gridnum].GetComponent<BoxCollider2D>().offset = new Vector2(1,1);
            gridsquares[gridnum].GetComponent<BoxCollider2D>().size = new Vector2(2,2);
            gridsquares[gridnum].GetComponent<MeshFilter>().mesh = gridmesh;
            gridsquares[gridnum].GetComponent<MeshRenderer>().material = material;
            float x = (float)(2 * (gridnum % 4 - 2));
            float y = (float)(2 * (1 - ((gridnum - gridnum % 4) / 4)));
            gridsquares[gridnum].transform.position = new Vector3(x,y);
        }
        return gridsquares;
    }

    public bool WinDetect()
    {
        //creating the custom quality grid
        //row win check
        //possible row and column check
        for (int row_or_coloumn = 0; row_or_coloumn < 2; row_or_coloumn++)
        {
            for (int place = 0; place < (row_or_coloumn*12)+4; place+=(row_or_coloumn*3)+1)
            {
                bool isempty = false;
                int[] items_in_a_row = new int[4];
                for (int collectornum = 1; collectornum < 5; collectornum++)
                {
                    if (grid[place + collectornum*((row_or_coloumn*-3)+4)-1] != 16)
                    {
                        items_in_a_row[collectornum-1] = grid[place + collectornum*((row_or_coloumn*-3)+4)-1];
                    }
                    else
                    {
                        isempty = true;
                        break;
                    }
                }
                if (isempty) {continue;}
                else if ((items_in_a_row[0]^items_in_a_row[1]^items_in_a_row[2]^items_in_a_row[3]) == 0)
                {
                    return true;
                }
            }
        }
        /*for (int row = 0; row < 16; row+=4)
        {
            if (!(sgrid[row] ^ sgrid[row+1] ^ sgrid[row+2] ^ sgrid[row+3]))
            {
                return true;
            }
        }
        //column win check
        for (int column = 0; column < 4; column++)
        {
            if (!(sgrid[column] ^ sgrid[column+4] ^ sgrid[column+8] ^ sgrid[column+12]))
            {
                return true;
            }
        }*/
        //top to bottom diagonal win check
        if (((grid[0] ^ grid[5] ^ grid[10] ^ grid[15]) == 0) && (((grid[0] | grid[5] | grid[10] | grid[15]) & 16) != 16))
        {
            return true;
        }
        //bottom to top diagonal win check
        if (((grid[3] ^ grid[6] ^ grid[9] ^ grid[12]) == 0) && (((grid[3] | grid[6] | grid[9] | grid[12]) & 16) != 16))
        {
            return true;
        }
        return false;
    }


    GameObject[] Createpieces()
    {
        Sprite[] piecesprite = {piece0s, piece1s, piece2s, piece3s, piece4s, piece5s, piece6s, piece7s};
        GameObject[] pieces = new GameObject[16];
        for (int piece = 0; piece < 16; piece++)
        {
            pieces[piece] = new GameObject("piece" + piece.ToString(), typeof(SpriteRenderer), typeof(pieces), typeof(BoxCollider2D));
            pieces[piece].GetComponent<BoxCollider2D>().size = new Vector2(5,5);
            pieces[piece].GetComponent<SpriteRenderer>().sprite = piecesprite[piece%8];
            pieces[piece].GetComponent<pieces>().scriptholder = gameObject;
            if (piece < 8)
            {
                pieces[piece].GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f);
            }
            else
            {
                pieces[piece].GetComponent<Transform>().localScale = new Vector3(0.25f, 0.25f);
            }
        }
        return pieces;
    }

    public void adaptgrid(GameObject piecemoved, int gridnum)
    {
        pgridsquares[gridnum].GetComponent<gridsquare>().isactive = false;
        float x = (float)(gridsize * (gridnum % 4 - 1.5));
        float y = (float)(gridsize * (1.5 - ((gridnum - gridnum % 4) / 4)));
        piecemoved.transform.position = new Vector3(x, y, -1);
        piecemoved.GetComponent<pieces>().ongrid = true;
        Debug.Log("piecemoved");
    }

    public void setside()
    {
        float x;
        for (int i = 0; i < 16; i++)
        {
            
            if (i < 8)
            {
                x = (float)(2*(1.5 - ((i - i % 4) / 4))-10);
            }
            else
            {
                x = (float)(2*(1.5 - ((i - i % 4) / 4))+10);
            }
            float y = (float)(2*(i % 4 - 1.5));
            pieces[i].transform.position = new Vector3(x,y);
        }
    }

}