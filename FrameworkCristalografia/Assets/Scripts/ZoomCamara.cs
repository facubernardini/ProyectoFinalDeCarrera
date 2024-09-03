using UnityEngine;
using UnityEngine.UI;

public class ZoomCamara : MonoBehaviour
{
    public Camera camaraPrincipal;
    public Slider slider;

    void Update()
    {
        float zoom = slider.value;
        camaraPrincipal.fieldOfView = 60f - zoom;
    }
}
