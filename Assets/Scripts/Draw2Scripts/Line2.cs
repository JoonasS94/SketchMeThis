using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Line2 : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _points = new List<Vector2>();
    private GameObject PointCountObject;
    private Camera _cam;
    private GameObject RayCastHitDrawingTargetObject;

    void Start()
    {
        _collider.transform.position -= transform.position;
    }

    private void Awake()
    {
        _cam = Camera.main;
        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounter2");
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
        {
            return;
        }

        // Allow more drawing if in appropiate position AND more ink still left
        // Remember to change <PointCountX> depending on scene
        if (CanAppend(pos) && PointCountObject.GetComponent<PointCount2>().canDraw == true)
        {
            _points.Add(pos);

            _renderer.positionCount++;

            if (_renderer.positionCount >= 2)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "ScoreObjectTag")
                    {
                        // If visible line hits a score area disable that score parts MeshCollider and add 1 point to pointCalculation
                        RayCastHitDrawingTargetObject = hit.transform.gameObject;
                        RayCastHitDrawingTargetObject.GetComponent<MeshCollider>().enabled = false;
                        // Remember to change <PointCountX> depending on scene
                        PointCountObject.GetComponent<PointCount2>().PointTotalCounter += 1;
                        //Debug.Log("Score!");
                    }
                }
            }

            _renderer.SetPosition(_renderer.positionCount - 1, pos);
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
            // Calculates how much player has drawn and adds it as a sum to DrawingDistanceInTotal
            // Remember to change <PointCountX> depending on scene
            PointCountObject.GetComponent<PointCount2>().DrawingDistanceInTotal += (Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos));

            // Remember to change DrawManagerX.RESOLUTION depending on scene
            return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager2.RESOLUTION;
        }
    }
}