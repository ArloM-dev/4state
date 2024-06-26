using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;


public class gridsquare : MonoBehaviour
{
    
    public bool isactive = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Mainscript = GameObject.FindWithTag("MainCamera");
        GetComponent<MeshRenderer>().enabled = false;
        int[] grid = Mainscript.GetComponent<Board>().grid;
        if (grid[int.Parse(name)] == 16)
        {
            isactive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter()
    {
        if (isactive)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnMouseDown()
    {
        GameObject Mainscript = GameObject.FindWithTag("MainCamera");
        GameObject selected =  Mainscript.GetComponent<Main>().selected_piece;
        if (isactive && (selected != null))
        {
            Mainscript.GetComponent<Board>().adaptgrid(selected, int.Parse(name));
            Mainscript.GetComponent<Board>().grid[Int32.Parse(name)] = Int32.Parse((selected.name).Remove(0,5));
            Mainscript.GetComponent<Main>().selected_piece = null;
            isactive = false;
            if (Mainscript.GetComponent<Board>().WinDetect())
            {
                Gameover gameover = Mainscript.GetComponent<Board>().gameover;
                gameover.GameOver(true);
            }
        }
    }
}
