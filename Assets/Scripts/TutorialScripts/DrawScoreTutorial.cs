using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScoreTutorial : MonoBehaviour
{
    // The score that is transported for future use in other scenes (final score of all drawings)
    // Remember to change drawXScore depending on scene
    public float drawTutorialScore;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
