using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LineTutorial : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _points = new List<Vector2>();
    private GameObject PointCountObject;
    private Camera _cam;
    private GameObject RayCastHitDrawingTargetObject;
    private int RandomInkSound;
    private bool DrawingSoundActive = false;

    // tutorial objects
    private GameObject CompareTextGameObject;
    private GameObject CompareText2GameObject;
    private TMP_Text CompareTextTMP;
    private TMP_Text CompareText2TMP;

    void Start()
    {
        _collider.transform.position -= transform.position;

        CompareTextGameObject = GameObject.Find("CompareText");
        CompareTextTMP = CompareTextGameObject.GetComponent<TextMeshProUGUI>();
        CompareText2GameObject = GameObject.Find("CompareText2");
        CompareText2TMP = CompareText2GameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        _cam = Camera.main;
        // Remember to change ("PointCounterX") depending on scene
        PointCountObject = GameObject.Find("PointCounterTutorial");
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
        {
            return;
        }

        // Allow more drawing if in appropiate position AND more ink still left
        // Remember to change <PointCountX> depending on scene
        if (CanAppend(pos) && PointCountObject.GetComponent<PointCountTutorial>().canDraw == true)
        {
            _points.Add(pos);

            _renderer.positionCount++;

            StartCoroutine(DrawSoundRandomize());

            if (_renderer.positionCount >= 2)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                CompareTextGameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                CompareText2GameObject.GetComponent<TextMeshProUGUI>().enabled = true;

                if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "ScoreObjectTag")
                    {
                        // If visible line hits a score area disable that score parts MeshCollider and add 1 point to pointCalculation
                        RayCastHitDrawingTargetObject = hit.transform.gameObject;
                        RayCastHitDrawingTargetObject.GetComponent<MeshCollider>().enabled = false;
                        // Remember to change <PointCountX> depending on scene
                        PointCountObject.GetComponent<PointCountTutorial>().PointTotalCounter += 1;
                        //Debug.Log("Score!");
                    }
                }
            }

            _renderer.SetPosition(_renderer.positionCount - 1, pos);
            _collider.points = _points.ToArray();
        }
    }

    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0)
        {
            return true;
        }

        else
        {
            // Calculates how much player has drawn and adds it as a sum to DrawingDistanceInTotal
            // Remember to change <PointCountX> depending on scene
            //PointCountObject.GetComponent<PointCountTutorial>().DrawingDistanceInTotal += (Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos));

            // Remember to change DrawManagerX.RESOLUTION depending on scene
            return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManagerTutorial.RESOLUTION;
        }
    }

    IEnumerator DrawSoundRandomize()
    {
        if (DrawingSoundActive == false)
        {
            DrawingSoundActive = true;

            RandomInkSound = (Random.Range(1, 11));

            if (RandomInkSound == 1)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound1");
            }

            if (RandomInkSound == 2)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound2");
            }

            if (RandomInkSound == 3)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound3");
            }

            if (RandomInkSound == 4)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound4");
            }

            if (RandomInkSound == 5)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound5");
            }

            if (RandomInkSound == 6)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound6");
            }

            if (RandomInkSound == 7)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound7");
            }

            if (RandomInkSound == 8)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound8");
            }

            if (RandomInkSound == 9)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound9");
            }

            if (RandomInkSound == 10)
            {
                FindObjectOfType<AudioManager>().Play("SFX_InkSound10");
            }

            yield return new WaitForSeconds(1f);
            DrawingSoundActive = false;
        }
    }
}
