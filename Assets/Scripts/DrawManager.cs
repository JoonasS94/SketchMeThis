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

    public float horizontalInput;
    public float verticalInput;

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

        Debug.Log(mousePos.x);

        // Only allow drawing if the mouse is in the right half of the screen
        if (Input.GetMouseButtonDown(0) && PointCountBoolRef == true /* && mousePos.x > Screen.width / 2 */)
        {
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }

        if (Input.GetMouseButton(0) && PointCountBoolRef == true)
        {
            _currentLine.SetPosition(mousePos);
        }
    }
}