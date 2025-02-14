using UnityEngine;

public class ManagerObjetoAR : MonoBehaviour
{
    private bool permitirManipulacionObjeto, isTouching;
    private float rotationSpeed, scaleSpeed, escalado;
    
    void Start()
    {
        permitirManipulacionObjeto = false;
        isTouching = false;
        rotationSpeed = 15f;
        scaleSpeed = 0.1f;
        escalado = 1f;
    }

    void Update()
    {
        RotarObjetoAR();
        EscalarObjetoAR();
    }

    private void RotarObjetoAR()
    {
        if (permitirManipulacionObjeto && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        // Calcular el desplazamiento horizontal del toque
                        float deltaX = touch.deltaPosition.x;

                        // Rotar el objeto únicamente en el eje Y
                        float rotationY = -deltaX * rotationSpeed * Time.deltaTime;
                        transform.Rotate(Vector3.up, rotationY, Space.World); // Rotación en el eje Y global
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }

    private void EscalarObjetoAR()
    {
        if (permitirManipulacionObjeto && Input.touchCount == 2)
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

            escalado += -distanceDelta * scaleSpeed * Time.deltaTime;
            escalado = Mathf.Clamp(escalado, 0.6f, 1.5f);
            transform.localScale = new Vector3(escalado, escalado, escalado);
            
        }
    }

    public void RestablecerEscala()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        escalado = 1f;
    }

    public void ActivarManipulacionObjeto()
    {
        permitirManipulacionObjeto = true;
    }

    public void DesactivarManipulacionObjeto()
    {
        permitirManipulacionObjeto = false;
    }
}
