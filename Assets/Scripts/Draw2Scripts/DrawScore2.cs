using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScore2 : MonoBehaviour
{
    // The score that is transported for future use in other scenes (final score of all drawings)
    // Remember to change ("PointCounterX") depending on scene
    public float draw2Score;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
