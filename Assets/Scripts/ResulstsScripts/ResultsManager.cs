using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    private Camera _cam;

    private GameObject LetsCountResultsGameObject;
    private TMP_Text LetsCountResultsTMP;

    public GameObject Draw1PointsGameObject;
    private TMP_Text Draw1PointsTMP;

    public GameObject Draw2PointsGameObject;
    private TMP_Text Draw2PointsTMP;

    public GameObject Draw3PointsGameObject;
    private TMP_Text Draw3PointsTMP;

    public GameObject Draw4PointsGameObject;
    private TMP_Text Draw4PointsTMP;

    public GameObject EqualGameObject;
    private TMP_Text EqualTMP;

    public GameObject Points;
    public GameObject CalculationMarkings;

    public float firstScore;
    public float secondScore;
    public float thirdScore;
    public float fourthScore;
    public float finalPoints;

    public GameObject LeftStarIn;
    public GameObject LeftStarOut;
    public GameObject MiddleStarIn;
    public GameObject MiddleStarOut;
    public GameObject RightStarIn;
    public GameObject RightStarOut;

    public GameObject MainMenuScreenPictureGameObject;
    // specify the game object to start drawing scene
    public GameObject MainMenuScreenButtonGameObject;
    // the collider of the quit area object
    private PolygonCollider2D _MainMenuAreaCollider;

    public GameObject BackGround;
    public Material BackGroundMainMenuButton;

    void Start()
    {
        _cam = Camera.main;

        LetsCountResultsGameObject = GameObject.Find("LetsCountTheResultsText");
        LetsCountResultsTMP = LetsCountResultsGameObject.GetComponent<TextMeshProUGUI>();
        Draw1PointsTMP = Draw1PointsGameObject.GetComponent<TextMeshProUGUI>();
        Draw2PointsTMP = Draw2PointsGameObject.GetComponent<TextMeshProUGUI>();
        Draw3PointsTMP = Draw3PointsGameObject.GetComponent<TextMeshProUGUI>();
        Draw4PointsTMP = Draw4PointsGameObject.GetComponent<TextMeshProUGUI>();
        EqualTMP = EqualGameObject.GetComponent<TextMeshProUGUI>();

        // get the collider of the start drawing scene area object
        _MainMenuAreaCollider = MainMenuScreenButtonGameObject.GetComponent<PolygonCollider2D>();

        StartCoroutine(StartSet());
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // Main Menu button pressed
        if (Input.GetMouseButtonDown(0) && _MainMenuAreaCollider.OverlapPoint(mousePos))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    IEnumerator StartSet()
    {
        firstScore = GameObject.Find("Score1").GetComponent<DrawScore1>().draw1Score;
        secondScore = GameObject.Find("Score2").GetComponent<DrawScore2>().draw2Score;
        thirdScore = GameObject.Find("Score3").GetComponent<DrawScore3>().draw3Score;
        fourthScore = GameObject.Find("Score4").GetComponent<DrawScore4>().draw4Score;

        finalPoints = firstScore + secondScore + thirdScore + fourthScore;

        Destroy(GameObject.Find("Score1"));
        Destroy(GameObject.Find("Score2"));
        Destroy(GameObject.Find("Score3"));
        Destroy(GameObject.Find("Score4"));

        Draw1PointsTMP.text = "" + firstScore;
        Draw2PointsTMP.text = "" + secondScore;
        Draw3PointsTMP.text = "" + thirdScore;
        Draw4PointsTMP.text = "" + fourthScore;
        EqualTMP.text = "" + finalPoints;

        yield return new WaitForSeconds(1.5f);
        Draw1PointsGameObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Draw2PointsGameObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Draw3PointsGameObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Draw4PointsGameObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        EqualGameObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // 1 star performance (0% - 32%)
        if (finalPoints >= 0 && finalPoints <= 128)
        {
            Debug.Log("1 star performance");
            Points.gameObject.SetActive(false);
            CalculationMarkings.gameObject.SetActive(false);
            LeftStarOut.gameObject.SetActive(true);
            MiddleStarOut.gameObject.SetActive(true);
            RightStarOut.gameObject.SetActive(true);

            LeftStarIn.gameObject.SetActive(true);
        }

        // 2 star performance (33% - 65%)
        if (finalPoints >= 129 && finalPoints <= 260)
        {
            Debug.Log("2 star performance");
            Points.gameObject.SetActive(false);
            CalculationMarkings.gameObject.SetActive(false);
            LeftStarOut.gameObject.SetActive(true);
            MiddleStarOut.gameObject.SetActive(true);
            RightStarOut.gameObject.SetActive(true);

            LeftStarIn.gameObject.SetActive(true);
            MiddleStarIn.gameObject.SetActive(true);
        }

        // 3 star performance (66% =<)
        if (finalPoints >= 261)
        {
            Debug.Log("3 star performance");
            Points.gameObject.SetActive(false);
            CalculationMarkings.gameObject.SetActive(false);
            LeftStarOut.gameObject.SetActive(true);
            MiddleStarOut.gameObject.SetActive(true);
            RightStarOut.gameObject.SetActive(true);

            LeftStarIn.gameObject.SetActive(true);
            MiddleStarIn.gameObject.SetActive(true);
            RightStarIn.gameObject.SetActive(true);
        }

        BackGround.GetComponent<MeshRenderer>().material = BackGroundMainMenuButton;

        LetsCountResultsTMP.text = "Thanks for playing";
        MainMenuScreenPictureGameObject.gameObject.SetActive(true);
    }
}
