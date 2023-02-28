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
        if (PointTotalCounter == 9999)
        {
            canDraw = false;
            Debug.Log("Yli 10 maalauspistettä tehty!");
        }
    }
}
