using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawManager4 : MonoBehaviour
{
    // specify the game object to draw inside
    private GameObject drawArea;
    // the collider of the draw area object
    private PolygonCollider2D _drawAreaCollider;

    private Camera _cam;

    // Remember to change LineX depending on scene
    [SerializeField] private Line4 _linePrefab;
    public const float RESOLUTION = .005f;
    // Remember to change LineX depending on scene
    private Line4 _currentLine;
    private GameObject PointCountObject;
    private GameObject StartTextGameObject;
    private TMP_Text StartTextTMP;
    private GameObject CompareTextGameObject;
    private GameObject CompareText2GameObject;
    private TMP_Text CompareText2TMP;
    private GameObject DrawingObject;
    private float DrawingResult;
    public Slider inkLeftSliderFill;
    private GameObject InkLeftPercentageGameObject;

    // 100 / amount of mesh colliders in drawing
    // Remember to adjust value for each drawing
    private float ratioNumber = 2.63158f;

    private int RoundingToInt;
    private bool CompareResultsStarted = false;

    // specify the game object to stop drawing
    private GameObject stopDrawingArea;
    // the collider of the stop drawing area object
    private PolygonCollider2D _stopDrawingAreaCollider;

    // specify the game object to enter next drawing
    public GameObject nextDrawingArea;
    // the collider of the stop drawing area object
    private PolygonCollider2D _nextDrawingAreaCollider;

    // specify the game object to pause game
    public GameObject PauseButtonColliderGameObject;
    // the collider of the pause game object
    private PolygonCollider2D _pauseGameAreaCollider;

    // specify the game object to pause game
    public GameObject PauseScreenGameObject;
    // specify the game object to disable pause game (continue drawing)
    public GameObject DisablePauseScreenGameObject;
    // the collider of the stop drawing area object
    private PolygonCollider2D _disablePauseGameAreaCollider;

    // specify the game object to disable pause game (continue drawing)
    public GameObject RestartSceneScreenGameObject;
    // the collider of the stop drawing area object
    private PolygonCollider2D _restartSceneGameAreaCollider;

    // specify the game object to disable pause game (continue drawing)
    public GameObject MainMenuScreenGameObject;
    // the collider of the stop drawing area object
    private PolygonCollider2D _mainMenuSceneGameAreaCollider;

    public GameObject BackGround;
    public Material BackGroundStartingMaterial;
    public Material BackGroundGameplay01Material;
    public Material BackGroundGameplay02Material;

    void Start()
    {
        _cam = Camera.main;
        // get the collider of the draw area object
        drawArea = GameObject.Find("DrawingArea");
        _drawAreaCollider = drawArea.GetComponent<PolygonCollider2D>();

        // get the collider of the stop drawing area object
        stopDrawingArea = GameObject.Find("StopDrawingCollider");
        _stopDrawingAreaCollider = stopDrawingArea.GetComponent<PolygonCollider2D>();

        // get the collider of the next drawing game area object
        _nextDrawingAreaCollider = nextDrawingArea.GetComponent<PolygonCollider2D>();

        // get the collider of the pause game area object
        _pauseGameAreaCollider = PauseButtonColliderGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the disable pause game (continue drawing) area object
        _disablePauseGameAreaCollider = DisablePauseScreenGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the restart scene game area object
        _restartSceneGameAreaCollider = RestartSceneScreenGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the main menu game area object
        _mainMenuSceneGameAreaCollider = MainMenuScreenGameObject.GetComponent<PolygonCollider2D>();

        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounter4");

        StartTextGameObject = GameObject.Find("StartText");
        StartTextTMP = StartTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareTextGameObject = GameObject.Find("CompareText");
        CompareText2GameObject = GameObject.Find("CompareText2");
        CompareText2TMP = CompareText2GameObject.GetComponent<TextMeshProUGUI>();
        InkLeftPercentageGameObject = GameObject.Find("InkLeftPercentage");

        // Coroutine of showing original drawing started
        StartCoroutine(StartGamePlay());
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // Remember to change <PointCountX> depending on scene
        // Pause game button pressed
        if (Input.GetMouseButtonDown(0) && _pauseGameAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCount4>().canDraw == true)
        {
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress1");
            StartCoroutine(PauseGame());
        }

        // Disable Pause game (continue drawing phase) button pressed
        if (Input.GetMouseButtonDown(0) && _disablePauseGameAreaCollider.OverlapPoint(mousePos))
        {
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress2");
            StartCoroutine(DisablePauseGame());
        }

        // Restart scene button pressed
        if (Input.GetMouseButtonDown(0) && _restartSceneGameAreaCollider.OverlapPoint(mousePos))
        {
            // Remember to change "ScoreX" depending on scene
            // Prevent Score Objects spawning endless amount
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress3");
            Destroy(GameObject.Find("Score4"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Return to main menu button pressed
        if (Input.GetMouseButtonDown(0) && _mainMenuSceneGameAreaCollider.OverlapPoint(mousePos))
        {
            // Remember to add Destroy ScoreX as many as needed (depending on scene)
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress4");
            Destroy(GameObject.Find("Score1"));
            Destroy(GameObject.Find("Score2"));
            Destroy(GameObject.Find("Score3"));
            Destroy(GameObject.Find("Score4"));
            SceneManager.LoadScene("MainMenuScene");
        }

        // Next drawing button pressed
        if (Input.GetMouseButtonDown(0) && _nextDrawingAreaCollider.OverlapPoint(mousePos))
        {
            // NOTE: May give error if work on scene has not been yet started
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress1");
            SceneManager.LoadScene("ResultsScene");
        }

        // Remember to change <PointCountX> depending on scene
        // Stop drawing pressed while game is active
        if (Input.GetMouseButtonDown(0) && _stopDrawingAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCount4>().canDraw == true && PointCountObject.GetComponent<PointCount4>().gamePaused == false)
        {
            // Remember to change <PointCountX> depending on scene
            FindObjectOfType<AudioManager>().Play("SFX_ButtonPress2");
            PointCountObject.GetComponent<PointCount4>().DrawingDistanceInTotal = 999;
        }


        // Remember to change <PointCountX> depending on scene
        // Drawing
        if (_drawAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCount4>().canDraw == true && PointCountObject.GetComponent<PointCount4>().gamePaused == false)
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
        // Remember to change <PointCountX>.canDraw && <PointCountX>.StopChecking depending on scene
        if (PointCountObject.GetComponent<PointCount4>().canDraw == false && PointCountObject.GetComponent<PointCount4>().StopChecking == true && CompareResultsStarted == false)
        {
            CompareResultsStarted = true;
            StartCoroutine(CompareResults());
        }
    }

    IEnumerator PauseGame()
    {
        //inkLeftSliderFill.gameObject.SetActive(false);
        //InkLeftPercentageGameObject.gameObject.SetActive(false);
        PauseScreenGameObject.gameObject.SetActive(true);
        // Remember to change <PointCountX> depending on scene
        PointCountObject.GetComponent<PointCount4>().gamePaused = true;
        yield return new WaitForSeconds(0.25f);

    }

    IEnumerator DisablePauseGame()
    {
        Debug.Log("DisablePauseGame IEnumerator active. Continuing the game");
        inkLeftSliderFill.gameObject.SetActive(true);
        InkLeftPercentageGameObject.gameObject.SetActive(true);
        PauseScreenGameObject.gameObject.SetActive(false);
        // Remember to change <PointCountX> depending on scene
        PointCountObject.GetComponent<PointCount4>().gamePaused = false;
        yield return new WaitForSeconds(0.25f);

    }

    IEnumerator StartGamePlay()
    {
        FindObjectOfType<AudioManager>().Play("SFX_UI_TEXT_02");
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
        yield return new WaitForSeconds(1);
        StartTextGameObject.gameObject.SetActive(false);
        // Allow player to draw
        // Remember to change <PointCountX>.canDraw depending on scene

        BackGround.GetComponent<MeshRenderer>().material = BackGroundGameplay01Material;

        PointCountObject.GetComponent<PointCount4>().canDraw = true;
    }

    IEnumerator CompareResults()
    {
        FindObjectOfType<AudioManager>().Play("SFX_UI_TEXT_03");
        stopDrawingArea.SetActive(false);

        BackGround.GetComponent<MeshRenderer>().material = BackGroundStartingMaterial;

        yield return new WaitForSeconds(0.25f);
        CompareTextGameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        DrawingObject = GameObject.Find("DrawingObject");
        MeshRenderer[] meshRenderers = DrawingObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = true;
        }

        // Waiting amount so that player (may) get excited themselves
        yield return new WaitForSeconds(3.5f);

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = false;
        }

        // Remember to change <PointCountX> depending on scene
        DrawingResult = (PointCountObject.GetComponent<PointCount4>().PointTotalCounter * ratioNumber);

        // To prevent over 100 point (very unlikely) cases
        if (DrawingResult > 100)
        {
            DrawingResult = 100;
        }

        RoundingToInt = Mathf.RoundToInt(DrawingResult);
        // Transfer score data to permanent score history GameObject
        // Remember to change ("ScoreX) && <DrawScoreX> && drawXScore depending on scene
        GameObject.Find("Score4").GetComponent<DrawScore4>().draw4Score = RoundingToInt;
        //Debug.Log("Score:" + RoundingToInt);

        CompareTextGameObject.gameObject.SetActive(false);

        CompareText2TMP.text = RoundingToInt + " / 100";
        CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        yield return new WaitForSeconds(2.5f);

        FindObjectOfType<AudioManager>().Play("SFX_UI_TEXT_05");

        // 90 - 100 Score
        if (RoundingToInt >= 90)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFantastic work!";
            FindObjectOfType<AudioManager>().Play("SFX_Score90To100");
        }

        // 66 - 89 Score
        if (RoundingToInt >= 66 && RoundingToInt <= 89)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nGreat job!";
            FindObjectOfType<AudioManager>().Play("SFX_Score66To89");
        }

        // 30 - 65 Score
        if (RoundingToInt >= 30 && RoundingToInt <= 65)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nOkay";
            FindObjectOfType<AudioManager>().Play("SFX_Score30To65");
        }

        // 10 - 29 Score
        if (RoundingToInt >= 10 && RoundingToInt <= 29)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nCould be better";
            FindObjectOfType<AudioManager>().Play("SFX_Score10To29");
        }

        // 0 - 9 Score
        if (RoundingToInt >= 0 && RoundingToInt <= 9)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nYou can do better";
            FindObjectOfType<AudioManager>().Play("SFX_Score0To10");
        }

        yield return new WaitForSeconds(0.5f);

        BackGround.GetComponent<MeshRenderer>().material = BackGroundGameplay02Material;

        nextDrawingArea.SetActive(true);
    }
}
