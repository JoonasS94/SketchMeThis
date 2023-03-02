using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public GameObject drawArea; // specify the game object to draw inside

    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    public const float RESOLUTION = .1f;
    private Line _currentLine;

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
        PointCountBoolRef = PointCountObject.GetComponent<PointCount>().canDraw;

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "ScoreObjectTag")
            {
                RayCastHitDrawingTargetObject = hit.transform.gameObject;
                RayCastHitDrawingTargetObject.GetComponent<MeshCollider>().enabled = false;
                Debug.Log("Score!");
            }
        }

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
