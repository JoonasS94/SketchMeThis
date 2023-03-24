using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScore4 : MonoBehaviour
{
    // The score that is transported for future use in other scenes (final score of all drawings)
    // Remember to change drawXScore depending on scene
    public float draw4Score;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
