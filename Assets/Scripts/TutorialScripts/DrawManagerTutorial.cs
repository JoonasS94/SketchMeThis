using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawManagerTutorial : MonoBehaviour
{
    // specify the game object to draw inside
    private GameObject drawArea;
    // the collider of the draw area object
    private PolygonCollider2D _drawAreaCollider;

    private Camera _cam;

    // Remember to change LineX depending on scene
    [SerializeField] private LineTutorial _linePrefab;
    public const float RESOLUTION = .005f;
    // Remember to change LineX depending on scene
    private LineTutorial _currentLine;
    private GameObject PointCountObject;
    private GameObject StartTextGameObject;
    private GameObject CompareTextGameObject;
    private GameObject CompareText2GameObject;
    // tutorial object
    private GameObject CompareText3GameObject;

    private TMP_Text CompareTextTMP;
    private TMP_Text CompareText2TMP;
    // tutorial object
    private TMP_Text CompareText3TMP;

    private GameObject DrawingObject;
    private float DrawingResult;
    public Slider inkLeftSliderFill;
    private GameObject InkLeftPercentageGameObject;

    // 100 / amount of mesh colliders in drawing
    // Remember to adjust value for each drawing
    private float ratioNumber = 8.33f;

    private int RoundingToInt;
    private bool CompareResultsStarted = false;

    // specify the game object to stop drawing
    private GameObject stopDrawingArea;
    // the collider of the stop drawing area object
    private PolygonCollider2D _stopDrawingAreaCollider;

    // specify the game object to enter next drawing
    public GameObject finishTutorialArea;
    // the collider of the stop drawing area object
    private PolygonCollider2D _finishTutorialDrawingAreaCollider;

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
        _finishTutorialDrawingAreaCollider = finishTutorialArea.GetComponent<PolygonCollider2D>();

        // get the collider of the pause game area object
        _pauseGameAreaCollider = PauseButtonColliderGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the disable pause game (continue drawing) area object
        _disablePauseGameAreaCollider = DisablePauseScreenGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the restart scene game area object
        _restartSceneGameAreaCollider = RestartSceneScreenGameObject.GetComponent<PolygonCollider2D>();

        // get the collider of the main menu game area object
        _mainMenuSceneGameAreaCollider = MainMenuScreenGameObject.GetComponent<PolygonCollider2D>();

        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounterTutorial");

        StartTextGameObject = GameObject.Find("StartText");
        CompareTextGameObject = GameObject.Find("CompareText");
        CompareTextTMP = CompareTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareText2GameObject = GameObject.Find("CompareText2");
        CompareText2TMP = CompareText2GameObject.GetComponent<TextMeshProUGUI>();

        CompareText3GameObject = GameObject.Find("CompareText3");
        CompareText3TMP = CompareText3GameObject.GetComponent<TextMeshProUGUI>();

        InkLeftPercentageGameObject = GameObject.Find("InkLeftPercentage");

        // Coroutine of showing original drawing started
        StartCoroutine(StartGamePlay());
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // Remember to change <PointCountX> depending on scene
        // Pause game button pressed
        if (Input.GetMouseButtonDown(0) && _pauseGameAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCountTutorial>().canDraw == true)
        {
            StartCoroutine(PauseGame());
        }

        // Disable Pause game (continue drawing phase) button pressed
        if (Input.GetMouseButtonDown(0) && _disablePauseGameAreaCollider.OverlapPoint(mousePos))
        {
            StartCoroutine(DisablePauseGame());
        }

        // Restart scene button pressed
        if (Input.GetMouseButtonDown(0) && _restartSceneGameAreaCollider.OverlapPoint(mousePos))
        {
            // Remember to change "ScoreX" depending on scene
            // Prevent Score Objects spawning endless amount
            Destroy(GameObject.Find("ScoreTutorial"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Return to main menu button pressed
        if (Input.GetMouseButtonDown(0) && _mainMenuSceneGameAreaCollider.OverlapPoint(mousePos))
        {
            // Remember to add Destroy ScoreX as many as needed (depending on scene)
            Destroy(GameObject.Find("ScoreTutorial"));
            SceneManager.LoadScene("MainMenuScene");
        }

        // Next drawing button pressed
        if (Input.GetMouseButtonDown(0) && _finishTutorialDrawingAreaCollider.OverlapPoint(mousePos))
        {
            // NOTE: May give error if work on scene has not been yet started
            Destroy(GameObject.Find("ScoreTutorial"));
            SceneManager.LoadScene("Draw1");
        }

        // Remember to change <PointCountX> depending on scene
        // Stop drawing pressed while game is active
        if (Input.GetMouseButtonDown(0) && _stopDrawingAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCountTutorial>().canDraw == true && PointCountObject.GetComponent<PointCountTutorial>().gamePaused == false)
        {
            // Remember to change <PointCountX> depending on scene
            PointCountObject.GetComponent<PointCountTutorial>().DrawingDistanceInTotal = 999;
        }


        // Remember to change <PointCountX> depending on scene
        // Drawing
        if (_drawAreaCollider.OverlapPoint(mousePos) && PointCountObject.GetComponent<PointCountTutorial>().canDraw == true && PointCountObject.GetComponent<PointCountTutorial>().gamePaused == false)
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
        if (PointCountObject.GetComponent<PointCountTutorial>().canDraw == false && PointCountObject.GetComponent<PointCountTutorial>().StopChecking == true && CompareResultsStarted == false)
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
        CompareTextGameObject.gameObject.SetActive(false);
        CompareText2GameObject.gameObject.SetActive(false);
        // Remember to change <PointCountX> depending on scene
        PointCountObject.GetComponent<PointCountTutorial>().gamePaused = true;
        yield return new WaitForSeconds(0.25f);

    }

    IEnumerator DisablePauseGame()
    {
        Debug.Log("DisablePauseGame IEnumerator active. Continuing the game");
        inkLeftSliderFill.gameObject.SetActive(true);
        InkLeftPercentageGameObject.gameObject.SetActive(true);
        CompareTextGameObject.gameObject.SetActive(true);
        CompareText2GameObject.gameObject.SetActive(true);
        PauseScreenGameObject.gameObject.SetActive(false);
        // Remember to change <PointCountX> depending on scene
        PointCountObject.GetComponent<PointCountTutorial>().gamePaused = false;
        yield return new WaitForSeconds(0.25f);

    }

    IEnumerator StartGamePlay()
    {
        yield return new WaitForSeconds(5);
        StartTextGameObject.gameObject.SetActive(false);

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

        yield return new WaitForSeconds(0.1f);

        CompareTextGameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        // Allow player to draw
        // Remember to change <PointCountX>.canDraw depending on scene

        BackGround.GetComponent<MeshRenderer>().material = BackGroundGameplay01Material;

        PointCountObject.GetComponent<PointCountTutorial>().canDraw = true;
    }

    IEnumerator CompareResults()
    {
        stopDrawingArea.SetActive(false);

        BackGround.GetComponent<MeshRenderer>().material = BackGroundStartingMaterial;

        CompareTextGameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = false;

        yield return new WaitForSeconds(0.25f);

        CompareText3GameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        yield return new WaitForSeconds(10);

        CompareText3GameObject.GetComponent<TextMeshProUGUI>().enabled = false;

        CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        CompareText2TMP.text = "Comparing...";

        DrawingObject = GameObject.Find("DrawingObject");
        MeshRenderer[] meshRenderers = DrawingObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = true;
        }

        // Waiting amount so that player (may) get excited themselves
        yield return new WaitForSeconds(2.5f);

        foreach (MeshRenderer childRenderMeshes in meshRenderers)
        {
            childRenderMeshes.enabled = false;
        }

        // Remember to change <PointCountX> depending on scene
        DrawingResult = (PointCountObject.GetComponent<PointCountTutorial>().PointTotalCounter * ratioNumber);

        // To prevent over 100 point (very unlikely) cases
        if (DrawingResult > 100)
        {
            DrawingResult = 100;
        }

        RoundingToInt = Mathf.RoundToInt(DrawingResult);
        // Transfer score data to permanent score history GameObject
        // Remember to change ("ScoreX) && <DrawScoreX> && drawXScore depending on scene
        GameObject.Find("ScoreTutorial").GetComponent<DrawScoreTutorial>().drawTutorialScore = RoundingToInt;
        //Debug.Log("Score:" + RoundingToInt);

        CompareTextGameObject.gameObject.SetActive(false);

        CompareText2TMP.text = RoundingToInt + " / 100";
        CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = true;

        yield return new WaitForSeconds(2.5f);

        // 90 - 100 Score
        if (RoundingToInt >= 90)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFinish tutorial by pressing button on top left.";
        }

        // 66 - 89 Score
        if (RoundingToInt >= 66 && RoundingToInt <= 89)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFinish tutorial by pressing button on top left.";
        }

        // 30 - 65 Score
        if (RoundingToInt >= 30 && RoundingToInt <= 65)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFinish tutorial by pressing button on top left.";
        }

        // 10 - 29 Score
        if (RoundingToInt >= 10 && RoundingToInt <= 29)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFinish tutorial by pressing button on top left.";
        }

        // 0 - 9 Score
        if (RoundingToInt >= 0 && RoundingToInt <= 9)
        {
            CompareText2TMP.text = RoundingToInt + " / 100" + "\nFinish tutorial by pressing button on top left.";
        }

        yield return new WaitForSeconds(0.5f);

        BackGround.GetComponent<MeshRenderer>().material = BackGroundGameplay02Material;

        finishTutorialArea.SetActive(true);
    }
}
