using UnityEngine;

public class ManagerPanelCeldaHexagonal : MonoBehaviour
{
    public GameObject corteCapaSuperior, corteCapaIntermedia;

    void Start()
    {
        corteCapaSuperior.SetActive(false);
        corteCapaIntermedia.SetActive(false);
    }

    public void MostrarCorteCapaSuperior()
    {
        OcultarCorteCapaIntermedia();
        corteCapaSuperior.SetActive(true);
    }

    public void OcultarCorteCapaSuperior()
    {
        corteCapaSuperior.SetActive(false);
    }

    public void MostrarCorteCapaIntermedia()
    {
        OcultarCorteCapaSuperior();
        corteCapaIntermedia.SetActive(true);
    }

    public void OcultarCorteCapaIntermedia()
    {
        corteCapaIntermedia.SetActive(false);
    }
}
