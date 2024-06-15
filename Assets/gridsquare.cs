using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


public class gridsquare : MonoBehaviour
{

    
    
    public  bool isactive = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Mainscript = GameObject.FindWithTag("MainCamera");
        GetComponent<MeshRenderer>().enabled = false;
        int[] grid =  Mainscript.GetComponent<Board>().grid;
        if (grid[Int32.Parse(name)] == 16)
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
        if (isactive)
        {
            var Board = Mainscript.GetComponent<Board>();
            Board.adaptgrid(selected, Int32.Parse(name));
        }
    }
}
