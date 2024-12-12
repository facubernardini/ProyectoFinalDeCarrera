using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float minX = -1.5f, maxX = 1.5f;
    public float minY = 2f, maxY = 4.6f;
    public GameObject botonRestablecerCamara;
    private bool isMoving, permitirMovimientoCamara;
    private Camera camara;
    private float zoomSpeed, moveSpeed, minFOV, maxFOV;
    private Vector2 lastTouchPosition;

    void Start()
    {
        permitirMovimientoCamara = false;
        camara = GetComponent<Camera>();

        zoomSpeed = 0.06f;
        moveSpeed = 0.6f;

        minFOV = 28f;
        maxFOV = 55f;

        botonRestablecerCamara.SetActive(false);
    }

    void Update()
    {
        MovimientoCamara();
        ZoomCamara();
    }

    private void MovimientoCamara()
    {
        if (permitirMovimientoCamara && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isMoving = true;
            }
            else if (touch.phase == TouchPhase.Moved && isMoving)
            {
                Vector2 touchDelta = touch.position - lastTouchPosition;

                Vector3 newPosition = transform.position + new Vector3(-touchDelta.x * moveSpeed * Time.deltaTime,
                                                                       -touchDelta.y * moveSpeed * Time.deltaTime, 0);

                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                transform.position = newPosition;
                
                lastTouchPosition = touch.position;

                botonRestablecerCamara.SetActive(true);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isMoving = false;
            }
        }
    }

    private void ZoomCamara()
    {
        if (permitirMovimientoCamara && Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            float previousDistance = Vector2.Distance(
                touch1.position - touch1.deltaPosition,
                touch2.position - touch2.deltaPosition
            );

            float distanceDelta = previousDistance - currentDistance;

            camara.fieldOfView += distanceDelta * zoomSpeed;

            camara.fieldOfView = Mathf.Clamp(camara.fieldOfView, minFOV, maxFOV);

            botonRestablecerCamara.SetActive(true);
        } 
    }

    public void ActivarMovimientoCamara()
    {
        permitirMovimientoCamara = true;
    }

    public void DesactivarMovimientoCamara()
    {
        permitirMovimientoCamara = false;
    }

    public void RestablecerCamara()
    {
        camara.fieldOfView = 55f;
        transform.position = new Vector3(0f, 3.8f, -9f);

        botonRestablecerCamara.SetActive(false);
    }
}
