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

    void Start()
    {
        _cam = Camera.main;

        // get the collider of the play area object
        playArea = GameObject.Find("PlayArea");
        _playAreaCollider = playArea.GetComponent<PolygonCollider2D>();

        // get the collider of the credits area object
        creditsArea = GameObject.Find("CreditsArea");
        _creditsAreaCollider = creditsArea.GetComponent<PolygonCollider2D>();

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
            Debug.Log("Play");
        }

        // Credits pressed
        if (Input.GetMouseButtonDown(0) && _creditsAreaCollider.OverlapPoint(mousePos))
        {
            Debug.Log("Credits");
        }

        // Quit pressed
        if (Input.GetMouseButtonDown(0) && _quitAreaCollider.OverlapPoint(mousePos))
        {
            Application.Quit();
        }
    }
}
