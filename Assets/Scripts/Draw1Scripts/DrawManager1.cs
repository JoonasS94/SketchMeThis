using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager1 : MonoBehaviour
{
    public GameObject drawArea; // specify the game object to draw inside

    private Camera _cam;
    [SerializeField] private Line1 _linePrefab;
    public const float RESOLUTION = .005f;
    private Line1 _currentLine;

    private GameObject PointCountObject;
    private bool PointCountBoolRef;

    private GameObject RayCastHitDrawingTargetObject;

    private BoxCollider2D _drawAreaCollider; // the collider of the draw area object

    void Start()
    {
        _cam = Camera.main;
        PointCountObject = GameObject.Find("PointCounter");

        // get the collider of the draw area object
        _drawAreaCollider = drawArea.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Check the boolean status in every frame
        // Remember to change <PointCountX> depending on scene
        PointCountBoolRef = PointCountObject.GetComponent<PointCount1>().canDraw;

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (_drawAreaCollider.OverlapPoint(mousePos) && PointCountBoolRef == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
            }

            if (Input.GetMouseButton(0))
            {
                _currentLine.SetPosition(mousePos);
            }
        }
    }
}
