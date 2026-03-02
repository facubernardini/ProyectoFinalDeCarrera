using UnityEngine;
using UnityEngine.UI;

public class ZoomCamaraScroll : MonoBehaviour
{
    public Camera camara;
    public Slider slider;
    private float minFOV, maxFOV, zoomSpeed;

    void Start()
    {
        zoomSpeed = 18f;
        minFOV = 23f;
        maxFOV = 58f;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            camara.fieldOfView -= scroll * zoomSpeed;
            camara.fieldOfView = Mathf.Clamp(camara.fieldOfView, minFOV, maxFOV);

            slider.value = 58f - camara.fieldOfView;
        }
    }

    public void ZoomSlider()
    {
        camara.fieldOfView = 58f - slider.value;
    }
}
