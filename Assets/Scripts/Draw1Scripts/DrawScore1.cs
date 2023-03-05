using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScore1 : MonoBehaviour
{
    // The score that is transported for future use in other scenes (final score of all drawings)
    public float draw1Score;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
