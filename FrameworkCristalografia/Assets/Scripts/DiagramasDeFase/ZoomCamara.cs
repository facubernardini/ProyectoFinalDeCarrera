using UnityEngine;

public class ZoomCamara : MonoBehaviour
{
    public GameObject botonRestablecerCamara;
    private bool isMoving, permitirMovimientoCamara;
    private Camera camara;
    private float zoomSpeed, moveSpeed, minSize, maxSize;
    private float minX, maxX, minY, maxY;
    private Vector2 lastTouchPosition;

    void Start()
    {
        permitirMovimientoCamara = false;
        camara = GetComponent<Camera>();

        minX = 15f;
        maxX = 80f;

        minY = 15f;
        maxY = 80f;

        zoomSpeed = 0.1f;
        moveSpeed = 10f;

        minSize = 40f;
        maxSize = 100f;

        botonRestablecerCamara.SetActive(false);
    }

    void Update()
    {
        MovimientoCamara();
        Zoom();
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
                                                                       0f, -touchDelta.y * moveSpeed * Time.deltaTime);

                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.z = Mathf.Clamp(newPosition.z, minY, maxY);

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

    private void Zoom()
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

            camara.orthographicSize += distanceDelta * zoomSpeed;

            camara.orthographicSize = Mathf.Clamp(camara.orthographicSize, minSize, maxSize);

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
        camara.orthographicSize = 100f;
        transform.position = new Vector3(50f, 10f, 40f);

        botonRestablecerCamara.SetActive(false);
    }
}
