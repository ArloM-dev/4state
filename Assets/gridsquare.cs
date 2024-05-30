using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;


public class gridsquare : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        GameObject Mainscript = GameObject.FindWithTag("MainCamera");
        int[] grid =  Mainscript.GetComponent<Board>().grid;
        if (grid[Int32.Parse(name)] == 16)
        {
        GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
