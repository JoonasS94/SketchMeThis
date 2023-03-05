using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawManager1 : MonoBehaviour
{
    // specify the game object to draw inside
    private GameObject drawArea;
    // the collider of the draw area object
    private BoxCollider2D _drawAreaCollider;
    private Camera _cam;
    [SerializeField] private Line1 _linePrefab;
    public const float RESOLUTION = .005f;
    private Line1 _currentLine;
    private GameObject PointCountObject;

    private GameObject StartTextGameObject;
    private TMP_Text StartTextTMP;
    private GameObject CompareTextGameObject;
    private TMP_Text CompareTextTMP;

    private GameObject DrawingObject;

    private float DrawingResult;
    // 100 / amount of mesh colliders in drawing
    // Remember to adjust value for each drawing
    private float ratioNumber = 4.166667f;
    private int RoundingToInt;

    void Start()
    {
        _cam = Camera.main;
        drawArea = GameObject.Find("DrawingArea");
        // get the collider of the draw area object
        _drawAreaCollider = drawArea.GetComponent<BoxCollider2D>();

        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounter1");

        StartTextGameObject = GameObject.Find("StartText");
        StartTextTMP = StartTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareTextGameObject = GameObject.Find("CompareText");
        CompareTextTMP = CompareTextGameObject.GetComponent<TextMeshProUGUI>();

        // Coroutine of showing original drawing started
        StartCoroutine(StartGamePlay());
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // Remember to change <PointCountX> depending on scene
        if (_drawAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCount1>().canDraw == true)
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

        // When player has finished drawing start comparing/ scoring process
        if (PointCountObject.GetComponent<PointCount1>().canDraw == false && PointCountObject.GetComponent<PointCount1>().StopChecking == true)
        {
            StartCoroutine(CompareResults());
        }
    }

    IEnumerator StartGamePlay()
    {
        yield return new WaitForSeconds(3);
        StartTextGameObject.gameObject.SetActive(false);
        StartTextTMP.text = "Time to draw yourself!";

        DrawingObject = GameObject.Find("DrawingObject");
        MeshRenderer[] meshRenderers = DrawingObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = true;
        }

        yield return new WaitForSeconds(5);

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = false;
        }

        StartTextGameObject.gameObject.SetActive(true);
        // Allow player to draw
        PointCountObject.GetComponent<PointCount1>().canDraw = true;

        yield return new WaitForSeconds(1);

        StartTextGameObject.gameObject.SetActive(false);

    }

    IEnumerator CompareResults()
    {
        yield return new WaitForSeconds(0.25f);
        CompareTextGameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        DrawingObject = GameObject.Find("DrawingObject");
        MeshRenderer[] meshRenderers = DrawingObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = true;
        }

        // Remember to change <PointCountX> depending on scene
        DrawingResult = (PointCountObject.GetComponent<PointCount1>().PointTotalCounter * ratioNumber);
        RoundingToInt = Mathf.RoundToInt(DrawingResult);
        // Transfer score data to permanent score history GameObject
        // Remember to change ("ScoreX) && <DrawScoreX> && drawXScore depending on scene
        GameObject.Find("Score1").GetComponent<DrawScore1>().draw1Score = RoundingToInt;
        Debug.Log("Score:" + RoundingToInt);
    }
}
