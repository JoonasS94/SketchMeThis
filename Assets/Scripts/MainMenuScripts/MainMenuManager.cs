using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Camera _cam;

    // specify the game object to play
    private GameObject playArea;
    // the collider of the play area object
    private PolygonCollider2D _playAreaCollider;

    // specify the game object to credits
    private GameObject creditsArea;
    // the collider of the credits area object
    private PolygonCollider2D _creditsAreaCollider;

    // specify the game object to quit
    private GameObject quitArea;
    // the collider of the quit area object
    private PolygonCollider2D _quitAreaCollider;

    public GameObject CreditsScreen;
    // specify the game object to close credits scene
    public GameObject quitCreditsScreenButton;
    public GameObject quitCreditsTexture;
    // the collider of the quit area object
    private PolygonCollider2D _quitCreditsAreaCollider;

    public GameObject TutorialPromptScreen;
    // specify the game object to start tutorial scene
    public GameObject TutorialPromptYesScreenButton;
    // the collider of the start tutorial area object
    private PolygonCollider2D _TutorialPromptYesAreaCollider;
    // specify the game object to start drawing scene
    public GameObject TutorialPromptNoScreenButton;
    // the collider of the quit area object
    private PolygonCollider2D _TutorialPromptNoAreaCollider;

    void Start()
    {
        _cam = Camera.main;

        // get the collider of the play area object
        playArea = GameObject.Find("PlayArea");
        _playAreaCollider = playArea.GetComponent<PolygonCollider2D>();

        // get the collider of the start drawing scene area object
        _TutorialPromptYesAreaCollider = TutorialPromptYesScreenButton.GetComponent<PolygonCollider2D>();

        // get the collider of the start drawing scene area object
        _TutorialPromptNoAreaCollider = TutorialPromptNoScreenButton.GetComponent<PolygonCollider2D>();

        // get the collider of the credits area object
        creditsArea = GameObject.Find("CreditsArea");
        _creditsAreaCollider = creditsArea.GetComponent<PolygonCollider2D>();

        // get the collider of the quit credits area object
        _quitCreditsAreaCollider = quitCreditsScreenButton.GetComponent<PolygonCollider2D>();

        // get the collider of the quit area object
        quitArea = GameObject.Find("QuitArea");
        _quitAreaCollider = quitArea.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // Play pressed
        if (Input.GetMouseButtonDown(0) && _playAreaCollider.OverlapPoint(mousePos))
        {
            playArea.SetActive(false);
            creditsArea.SetActive(false);
            quitArea.SetActive(false);
            TutorialPromptScreen.SetActive(true);
            TutorialPromptYesScreenButton.SetActive(true);
            TutorialPromptNoScreenButton.SetActive(true);
        }

        // Tutorial Prompt Yes pressed
        if (Input.GetMouseButtonDown(0) && _TutorialPromptYesAreaCollider.OverlapPoint(mousePos))
        {
            Debug.Log("Yes");
        }

        // Tutorial Prompt No pressed
        if (Input.GetMouseButtonDown(0) && _TutorialPromptNoAreaCollider.OverlapPoint(mousePos))
        {
            // Load drawing scene 1
            SceneManager.LoadScene(1);
        }

        // Credits pressed
        if (Input.GetMouseButtonDown(0) && _creditsAreaCollider.OverlapPoint(mousePos))
        {
            playArea.SetActive(false);
            creditsArea.SetActive(false);
            quitArea.SetActive(false);
            CreditsScreen.SetActive(true);
            quitCreditsScreenButton.SetActive(true);
            quitCreditsTexture.SetActive(true);
        }

        // Quit Credits pressed
        if (Input.GetMouseButtonDown(0) && _quitCreditsAreaCollider.OverlapPoint(mousePos))
        {
            CreditsScreen.SetActive(false);
            quitCreditsScreenButton.SetActive(false);
            quitCreditsTexture.SetActive(false);
            playArea.SetActive(true);
            creditsArea.SetActive(true);
            quitArea.SetActive(true);
        }

        // Quit pressed
        if (Input.GetMouseButtonDown(0) && _quitAreaCollider.OverlapPoint(mousePos))
        {
            Application.Quit();
        }
    }
}
