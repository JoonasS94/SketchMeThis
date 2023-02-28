using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    public const float RESOLUTION = .1f;
    private Line _currentLine;

    private GameObject PointCountObject;
    private bool PointCountBoolRef;

    void Start()
    {
        _cam = Camera.main;
        PointCountObject = GameObject.Find("PointCounter");
    }


    void Update()
    {
        // Check the boolean status in every frame
        PointCountBoolRef = PointCountObject.GetComponent<PointCount>().canDraw;

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && PointCountBoolRef == true)
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                //Code for action on mouse moving left
                print("Mouse moved left");
            }

            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0) && PointCountBoolRef == true)
        {
            _currentLine.SetPosition(mousePos);

        }
    }
}
