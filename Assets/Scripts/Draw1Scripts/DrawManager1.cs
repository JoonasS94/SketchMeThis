using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawManager1 : MonoBehaviour
{
    // specify the game object to draw inside
    private GameObject drawArea;
    // the collider of the draw area object
    private PolygonCollider2D _drawAreaCollider;
    private Camera _cam;
    [SerializeField] private Line1 _linePrefab;
    public const float RESOLUTION = .005f;
    private Line1 _currentLine;
    private GameObject PointCountObject;

    private GameObject StartTextGameObject;
    private TMP_Text StartTextTMP;
    private GameObject CompareTextGameObject;
    private TMP_Text CompareTextTMP;
    private GameObject CompareText2GameObject;
    private TMP_Text CompareText2TMP;

    private GameObject DrawingObject;

    private float DrawingResult;
    // 100 / amount of mesh colliders in drawing
    // Remember to adjust value for each drawing
    private float ratioNumber = 4.166667f;
    private int RoundingToInt;

    private bool CompareResultsStarted = false;

    void Start()
    {
        _cam = Camera.main;
        drawArea = GameObject.Find("DrawingArea");
        // get the collider of the draw area object
        _drawAreaCollider = drawArea.GetComponent<PolygonCollider2D>();

        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounter1");

        StartTextGameObject = GameObject.Find("StartText");
        StartTextTMP = StartTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareTextGameObject = GameObject.Find("CompareText");
        CompareTextTMP = CompareTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareText2GameObject = GameObject.Find("CompareText2");
        CompareText2TMP = CompareText2GameObject.GetComponent<TextMeshProUGUI>();

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
        if (PointCountObject.GetComponent<PointCount1>().canDraw == false && PointCountObject.GetComponent<PointCount1>().StopChecking == true && CompareResultsStarted == false)
        {
            CompareResultsStarted = true;
            StartCoroutine(CompareResults());
        }
    }

    IEnumerator StartGamePlay()
    {
        yield return new WaitForSeconds(3);
        StartTextGameObject.gameObject.SetActive(false);
        StartTextTMP.text = "Now draw it yourself!";

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

        // Waiting amount so that player (may) get excited themselves
        yield return new WaitForSeconds(2.5f);

        // Remember to change <PointCountX> depending on scene
        DrawingResult = (PointCountObject.GetComponent<PointCount1>().PointTotalCounter * ratioNumber);

        // To prevent over 100 point (very unlikely) cases
        if (DrawingResult > 100)
        {
            DrawingResult = 100;
        }

        RoundingToInt = Mathf.RoundToInt(DrawingResult);
        // Transfer score data to permanent score history GameObject
        // Remember to change ("ScoreX) && <DrawScoreX> && drawXScore depending on scene
        GameObject.Find("Score1").GetComponent<DrawScore1>().draw1Score = RoundingToInt;
        //Debug.Log("Score:" + RoundingToInt);

        CompareTextGameObject.gameObject.SetActive(false);

        CompareText2TMP.text = RoundingToInt + " / 100";
        CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        yield return new WaitForSeconds(2.5f);

        // 90 - 100 Score
        if (RoundingToInt >= 90)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFantastic work!";
        }

        // 66 - 89 Score
        if (RoundingToInt >= 66 && RoundingToInt <= 89)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nGreat job!";
        }

        // 30 - 65 Score
        if (RoundingToInt >= 30 && RoundingToInt <= 65)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nOkay";
        }

        // 10 - 29 Score
        if (RoundingToInt >= 10 && RoundingToInt <= 29)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nCould be better";
        }

        // 0 - 9 Score
        if (RoundingToInt >= 0 && RoundingToInt <= 9)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nTry again";
        }
    }
}
