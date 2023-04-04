using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointCount4 : MonoBehaviour
{
    public int PointTotalCounter = 0;
    public float DrawingDistanceInTotal = 0;
    public bool canDraw = false;
    public bool StopChecking = false;
    public bool gamePaused = false;

    private GameObject InkLeftPercentageGameObject;
    private TMP_Text InkLeftPercentageTextTMP;

    // Remember to change X.Xf depending on scene drawing
    private float MaximumDrawingDistance = 85.5f;
    private float inkLeftCalculation;
    private int inkLeftCalculationDec;

    public Slider inkLeftSliderFill;

    private void Start()
    {
        Application.targetFrameRate = 60;
        InkLeftPercentageGameObject = GameObject.Find("InkLeftPercentage");
        InkLeftPercentageTextTMP = InkLeftPercentageGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDraw == true)
        {
            // Calculate how much ink left and show it to player as XXX %
            inkLeftCalculation = ((MaximumDrawingDistance - DrawingDistanceInTotal) / MaximumDrawingDistance * 100);
            inkLeftCalculationDec = Mathf.RoundToInt(inkLeftCalculation);
            inkLeftSliderFill.value = inkLeftCalculationDec;

            // If player consumes ink more than 0 % show still 0 % ink left
            if (inkLeftCalculationDec < 0)
            {
                canDraw = false;
                inkLeftCalculationDec = 0;
                inkLeftSliderFill.value = inkLeftCalculationDec;
            }
            InkLeftPercentageTextTMP.text = inkLeftCalculationDec + " %";
        }

        if (DrawingDistanceInTotal >= MaximumDrawingDistance && StopChecking == false)
        {
            // Out of ink
            canDraw = false;
            StopChecking = true;
            inkLeftSliderFill.gameObject.SetActive(false);
        }
    }
}
