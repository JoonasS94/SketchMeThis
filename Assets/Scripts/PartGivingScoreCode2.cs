/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartGivingScoreCode2 : MonoBehaviour
{
    private Camera _cam;
    private MeshCollider _meshCollider;

    void Start()
    {
        _cam = Camera.main;

        _meshCollider = this.gameObject.GetComponent<MeshCollider>();
    }

    void Update()
    {

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (_meshCollider.OverlapPoint(mousePos))
        {

        }
    }
}*/
