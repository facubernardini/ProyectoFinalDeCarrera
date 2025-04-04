using UnityEngine;

public class ManagerCamara : MonoBehaviour
{
    public Transform target; // Objeto al cual la cámara orbitará, en nuestro caso el centro de la celda unitaria
    private float rotationSpeed, currentYAngle, distance;
    private float zoomSpeed, minFOV, maxFOV;
    private bool isRotating, permitirRotacionCamara;
    private Vector3 lastMousePosition;
    private Camera camaraPrincipal;
    

    void Start()
    {
        camaraPrincipal = GetComponent<Camera>();

        // Rotacion
        isRotating = false;
        permitirRotacionCamara = false;
        rotationSpeed = 20f; // Velocidad de rotacion de la camara
        currentYAngle = 0f;
        distance = 5.5f; // Distancia entre la camara y el centro de la celda unitaria
        transform.LookAt(target);

        // Zoom
        zoomSpeed = 5f;
        minFOV = 23f;
        maxFOV = 58f;

    }

    void Update()
    {
        RotarCamara();
        ZoomCamara();
    }

    public void ActivarRotacionCamara()
    {
        permitirRotacionCamara = true;
    }

    public void DesactivarRotacionCamara()
    {
        permitirRotacionCamara = false;
    }

    private void RotarCamara()
    {
        if (permitirRotacionCamara && Input.touchCount == 1) // Verifica que el usuario esté presionando en el area correcta para rotar la cámara
        {
            if (Input.GetMouseButtonDown(0))
            {
                isRotating = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isRotating = false;
            }

            // Rotate the camera only when the left mouse button is held down and dragged
            if (isRotating)
            {
                // Calculate the mouse movement
                Vector2 deltaMouse = Input.mousePosition - lastMousePosition;

                // Rotate the camera around the Y-axis (horizontal rotation)
                float horizontalRotation = deltaMouse.x * rotationSpeed * Time.deltaTime;
                camaraPrincipal.transform.RotateAround(target.position, Vector3.up, horizontalRotation);

                // Calculate the vertical rotation
                float verticalRotation = deltaMouse.y * rotationSpeed * Time.deltaTime;

                // Apply and clamp the vertical rotation angle (Y-axis)
                currentYAngle = Mathf.Clamp(currentYAngle - verticalRotation, -89f, 89f);

                // Create a new direction based on clamped vertical rotation
                Quaternion rotation = Quaternion.Euler(currentYAngle, transform.eulerAngles.y, 0f);
                Vector3 direction = rotation * Vector3.forward * distance;

                // Set the new position and orientation of the camera
                camaraPrincipal.transform.position = target.position - direction;
                camaraPrincipal.transform.LookAt(target);

                // Update the start position for the next frame
                lastMousePosition = Input.mousePosition;
            }
        }
    }

    private void ZoomCamara()
    {
        // Detectar si hay al menos dos toques en la pantalla
        if (permitirRotacionCamara && Input.touchCount == 2)
        {
            // Obtener los dos toques
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calcular la distancia actual entre los dos toques
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            // Calcular la distancia anterior entre los dos toques
            float previousDistance = Vector2.Distance(
                touch1.position - touch1.deltaPosition,
                touch2.position - touch2.deltaPosition
            );

            // La diferencia entre las distancias anterior y actual
            float distanceDelta = previousDistance - currentDistance;

            // Ajustar el Field of View de la cámara
            camaraPrincipal.fieldOfView += distanceDelta * zoomSpeed * Time.deltaTime;

            // Restringir el Field of View dentro de los límites establecidos
            camaraPrincipal.fieldOfView = Mathf.Clamp(camaraPrincipal.fieldOfView, minFOV, maxFOV);
        }

        
    }
}