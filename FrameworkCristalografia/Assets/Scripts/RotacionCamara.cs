using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotacionCamara : MonoBehaviour
{
    private Vector2 pastFingerPosition = Vector2.zero;
    private Vector2 currentFingerPosition = Vector2.zero;
    private Vector2 rotation = Vector2.zero;
    public float speed;
    private bool permitirRotacionCamara;

    private void SetPastFingerPosition()
    {
        if (Input.touchCount == 1)
        {
            foreach (Touch t in Input.touches)
            {
                pastFingerPosition = t.position;
            }
        }
    }

    private void SetCurrentFingerPosition()
    {
        if (Input.touchCount == 1)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Moved)
                {
                    currentFingerPosition.x = pastFingerPosition.x - t.position.x;
                    currentFingerPosition.y = pastFingerPosition.y - t.position.y;
                }
            }
        }
    }

    private void RotateElement()
    {
        if (Input.touchCount == 1)
        {
            rotation.y += currentFingerPosition.x;
            rotation.x += -currentFingerPosition.y;

            transform.localEulerAngles = (Vector2) rotation * speed;
        }
        
    }

    void Start()
    {
        permitirRotacionCamara = false;
    }

    void Update()
    {
        if (permitirRotacionCamara)
        {
            SetPastFingerPosition();
            RotateElement();
        }
    }

    void FixedUpdate()
    {
        if (permitirRotacionCamara)
        {
            SetCurrentFingerPosition();
        }
    }

    public void ActivarRotacionCamara()
    {
        permitirRotacionCamara = true;
    }

    public void DesactivarRotacionCamara()
    {
        permitirRotacionCamara = false;
    }

}