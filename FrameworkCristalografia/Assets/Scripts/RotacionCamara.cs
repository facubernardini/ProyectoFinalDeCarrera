using UnityEngine;

public class RotacionCamara : MonoBehaviour
{
    public Transform target; // Objeto al cual la cámara orbitará, en nuestro caso el centro de la celda unitaria
    private float speed, currentYAngle, distance;
    private bool isRotating, permitirRotacionCamara;
    private Vector3 lastMousePosition;
    

    void Start()
    {
        isRotating = false;
        permitirRotacionCamara = false;
        speed = 25f; // Velocidad de rotacion de la camara
        currentYAngle = 0f;
        distance = 5.5f; // Distancia entre la camara y el centro de la celda unitaria
        transform.LookAt(target);
    }

    void Update()
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
                float horizontalRotation = deltaMouse.x * speed * Time.deltaTime;
                transform.RotateAround(target.position, Vector3.up, horizontalRotation);

                // Calculate the vertical rotation
                float verticalRotation = deltaMouse.y * speed * Time.deltaTime;

                // Apply and clamp the vertical rotation angle (Y-axis)
                currentYAngle = Mathf.Clamp(currentYAngle - verticalRotation, -89f, 89f);

                // Create a new direction based on clamped vertical rotation
                Quaternion rotation = Quaternion.Euler(currentYAngle, transform.eulerAngles.y, 0f);
                Vector3 direction = rotation * Vector3.forward * distance;

                // Set the new position and orientation of the camera
                transform.position = target.position - direction;
                transform.LookAt(target);

                // Update the start position for the next frame
                lastMousePosition = Input.mousePosition;
            }
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