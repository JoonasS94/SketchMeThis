using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCount1 : MonoBehaviour
{
    public int PointTotalCounter = 0;
    public float DrawingDistanceInTotal = 0;
    public bool canDraw = false;
    public bool StopChecking = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // Remember to change >= X.Xf depending on scene drawing
        if (DrawingDistanceInTotal >= 27.5f && StopChecking == false)
        {
            // Out of ink
            canDraw = false;
            StopChecking = true;
        }
    }
}
