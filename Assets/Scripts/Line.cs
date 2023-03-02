using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _points = new List<Vector2>();

    private GameObject PointCountObject;
    private bool PointCountBoolRef;

    private Camera _cam;

    private GameObject RayCastHitDrawingTargetObject;

    void Start()
    {
        _collider.transform.position -= transform.position;

    }

    private void Awake()
    {
        _cam = Camera.main;
        PointCountObject = GameObject.Find("PointCounter");
    }


    void Update()
    {
        // Check the boolean status in every frame
        PointCountBoolRef = PointCountObject.GetComponent<PointCount>().canDraw;
    }

    public void SetPosition(Vector2 pos)
    {

        if (!CanAppend(pos))
        {
            Debug.Log("Line.cs Debug rivi 37");
            return;
        }
        
        // Allow more drawing if in appropiate position AND more ink still left
        if (CanAppend(pos) && PointCountBoolRef == true)
        {
            _points.Add(pos);
            Debug.Log("Line.cs Debug rivi 45");

            _renderer.positionCount++;

            if (_renderer.positionCount >= 2)
            {
                Debug.Log("Jatkuvaa vivaa");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.tag == "ScoreObjectTag")
                    {
                        RayCastHitDrawingTargetObject = hit.transform.gameObject;
                        RayCastHitDrawingTargetObject.GetComponent<MeshCollider>().enabled = false;
                        Debug.Log("Score!");
                    }
                }
            }

            _renderer.SetPosition(_renderer.positionCount - 1, pos);

            PointCountObject.GetComponent<PointCount>().PointTotalCounter += 1;

            _collider.points = _points.ToArray();
        }
    }

    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0)
        {
            return true;
        }

        else
        {
            return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
        }


    }
}
