using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBackGroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // maintain main menu song active
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
