using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCount1 : MonoBehaviour
{
    public int PointTotalCounter = 0;
    public float DrawingDistanceInTotal = 0;

    public bool canDraw = true;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        if (DrawingDistanceInTotal >= 27.5f)
        {
            canDraw = false;
            Debug.Log("Ink loppui! Tarkista pisteet!");
        }
    }
}
