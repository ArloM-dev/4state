using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pieces : MonoBehaviour
{

    public bool ongrid = false;

    public GameObject scriptholder;

    void OnMouseDown()
    {
        if (ongrid == false)
        {
            scriptholder.GetComponent<Main>().selected_piece = gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
