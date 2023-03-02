using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCount : MonoBehaviour
{
    public int PointTotalCounter = 0;

    public bool canDraw = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 125
        if (PointTotalCounter == 1025)
        {
            canDraw = false;
            Debug.Log("Ink loppui! Tarkista pisteet!");
        }
    }
}
