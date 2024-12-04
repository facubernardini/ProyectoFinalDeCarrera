using UnityEngine;
using UnityEngine.UI;

public class ManagerVistaCorte : MonoBehaviour
{
    public GameObject iconoLoading, vistaCorte;
    public Button botonGenerarCorte;
    void Start()
    {
        iconoLoading.SetActive(false);
        vistaCorte.SetActive(true);
        botonGenerarCorte.interactable = true;
    }

    public void MostrarVistaCorte()
    {
        vistaCorte.SetActive(true);
    }

    public void OcultarVistaCorte()
    {
        vistaCorte.SetActive(false);
    }

    public void ActivarBotonGenerarCorte()
    {
        botonGenerarCorte.interactable = true;
    }

    public void DesactivarBotonGenerarCorte()
    {
        botonGenerarCorte.interactable = false;
    }

    public void MostrarIconoLoding()
    {
        iconoLoading.SetActive(true);
    }

    public void OcultarIconoLoading()
    {
        iconoLoading.SetActive(false);
    }
}
