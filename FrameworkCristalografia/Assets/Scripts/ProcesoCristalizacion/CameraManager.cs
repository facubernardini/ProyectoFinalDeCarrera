using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [Header("Límites de Movimiento")]
    public float minX = -1.5f;
    public float maxX = 1.5f;
    public float minY = 2f;
    public float maxY = 4.6f;

    [Header("Referencias UI")]
    public GameObject botonRestablecerCamara;
    public Slider sliderZoom; 

    private bool isMoving, permitirMovimientoCamara;
    private Camera camara;
    private float zoomSpeed, moveSpeed, minFOV, maxFOV;
    private Vector2 lastInputPosition;

    void Start()
    {
        permitirMovimientoCamara = false;
        camara = GetComponent<Camera>();

        zoomSpeed = 20f; 
        moveSpeed = 0.6f;

        minFOV = 24f; 
        maxFOV = 55f; 

        if (sliderZoom != null)
        {
            sliderZoom.minValue = 0f;
            sliderZoom.maxValue = 1f;
            sliderZoom.value = FOVToSlider(camara.fieldOfView);
            sliderZoom.onValueChanged.AddListener(OnSliderZoomChanged);
        }

        botonRestablecerCamara.SetActive(false);
    }

    void Update()
    {
        MovimientoCamara();
        ZoomCamaraPinch(); // Móvil
        ZoomCamaraMouse(); // Desktop
    }

    private float FOVToSlider(float fov) => Mathf.InverseLerp(maxFOV, minFOV, fov);
    private float SliderToFOV(float value) => Mathf.Lerp(maxFOV, minFOV, value);

    private void MovimientoCamara()
    {
        if (!permitirMovimientoCamara) return;

        // --- LÓGICA PARA DESKTOP (Mouse) Y MÓVIL (Touch) ---
        bool inputBegan = Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began);
        bool inputMoving = Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved);
        bool inputEnded = Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended);

        Vector2 currentInputPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

        if (inputBegan)
        {
            lastInputPosition = currentInputPos;
            isMoving = true;
        }
        else if (inputMoving && isMoving)
        {
            Vector2 delta = currentInputPos - lastInputPosition;
            
            // Calculamos la nueva posición invirtiendo el delta para que el arrastre sea intuitivo
            Vector3 newPosition = transform.position + new Vector3(-delta.x * moveSpeed * Time.deltaTime,
                                                                   -delta.y * moveSpeed * Time.deltaTime, 0);

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
            lastInputPosition = currentInputPos;
            
            if (delta.magnitude > 0.1f) botonRestablecerCamara.SetActive(true);
        }
        else if (inputEnded)
        {
            isMoving = false;
        }
    }

    private void ZoomCamaraMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float nuevoFOV = camara.fieldOfView - (scroll * zoomSpeed);
            camara.fieldOfView = Mathf.Clamp(nuevoFOV, minFOV, maxFOV);

            ActualizarSliderVisualmente();
            botonRestablecerCamara.SetActive(true);
        }
    }

    private void ZoomCamaraPinch()
    {
        if (permitirMovimientoCamara && Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch1.position, touch2.position);
            float previousDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
            
            // Ajustamos la sensibilidad del pinch para que coincida con la rapidez de la rueda
            float distanceDelta = (previousDistance - currentDistance) * 0.05f; 

            float nuevoFOV = camara.fieldOfView + (distanceDelta * zoomSpeed);
            camara.fieldOfView = Mathf.Clamp(nuevoFOV, minFOV, maxFOV);

            ActualizarSliderVisualmente();
            botonRestablecerCamara.SetActive(true);
        }
    }

    private void ActualizarSliderVisualmente()
    {
        if (sliderZoom != null)
        {
            sliderZoom.onValueChanged.RemoveListener(OnSliderZoomChanged);
            sliderZoom.value = FOVToSlider(camara.fieldOfView);
            sliderZoom.onValueChanged.AddListener(OnSliderZoomChanged);
        }
    }

    public void OnSliderZoomChanged(float value)
    {
        camara.fieldOfView = SliderToFOV(value);
        if (!botonRestablecerCamara.activeSelf) botonRestablecerCamara.SetActive(true);
    }

    public void RestablecerCamara()
    {
        camara.fieldOfView = 55f;
        transform.position = new Vector3(0f, 3.8f, -9f);
        if (sliderZoom != null) sliderZoom.value = 0f;
        botonRestablecerCamara.SetActive(false);
    }

    public void ActivarMovimientoCamara() => permitirMovimientoCamara = true;
    public void DesactivarMovimientoCamara() => permitirMovimientoCamara = false;
}