using UnityEngine;
using UnityEngine.UI;

public class ZoomCamara : MonoBehaviour
{
    public GameObject botonRestablecerCamara;
    public Slider slider;
    private Vector2 minPosition = new Vector2(15f, 15f);
    private Vector2 maxPosition = new Vector2(80f, 80f);
    private bool isDragging, permitirMovimientoCamara;
    private Camera camara;
    private float zoomSpeed, moveSpeed, minSize, maxSize;
    private Vector3 lastMousePosition;

    void Start()
    {
        permitirMovimientoCamara = false;
        camara = GetComponent<Camera>();

        zoomSpeed = 26f;
        moveSpeed = 1f;

        minSize = 30f;
        maxSize = 100f;

        botonRestablecerCamara.SetActive(false);
    }

    void Update()
    {
        MovimientoCamara();
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            camara.orthographicSize -= scroll * zoomSpeed;
            camara.orthographicSize = Mathf.Clamp(camara.orthographicSize, minSize, maxSize);

            slider.value = 100f - camara.orthographicSize;

            botonRestablecerCamara.SetActive(true);
        }
    }

    private void MovimientoCamara()
    {
        if (permitirMovimientoCamara)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = GetMouseWorldPosition();
                isDragging = true;
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector3 currentMousePosition = GetMouseWorldPosition();
                Vector3 delta = lastMousePosition - currentMousePosition;

                // Solo mover en X y Z (mantener Y fijo)
                Vector3 newPosition = camara.transform.position + new Vector3(delta.x, 0, delta.z) * moveSpeed;

                // Limitar dentro del rango permitido
                newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
                newPosition.z = Mathf.Clamp(newPosition.z, minPosition.y, maxPosition.y);
                newPosition.y = camara.transform.position.y; // mantener altura fija

                camara.transform.position = newPosition;

                lastMousePosition = GetMouseWorldPosition();
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            botonRestablecerCamara.SetActive(true);
        }
    }

    // Obtiene la posición del mouse proyectada sobre el plano XZ
    private Vector3 GetMouseWorldPosition()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero); // plano horizontal Y=0
        Ray ray = camara.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    public void ZoomSlider()
    {
        camara.orthographicSize = 100f - slider.value;
        botonRestablecerCamara.SetActive(true);
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
        slider.value = 0f;

        botonRestablecerCamara.SetActive(false);
    }
}