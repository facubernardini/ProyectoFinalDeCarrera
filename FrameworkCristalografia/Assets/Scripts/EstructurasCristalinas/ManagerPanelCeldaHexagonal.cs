using UnityEngine;

public class ManagerPanelCeldaHexagonal : MonoBehaviour
{
    public GameObject corteCapaSuperior, corteCapaIntermedia;
    public GameObject planoSuperior, planoIntermedio;

    void Start()
    {
        corteCapaSuperior.SetActive(false);
        corteCapaIntermedia.SetActive(false);
        planoSuperior.SetActive(false);
        planoIntermedio.SetActive(false);
    }

    public void MostrarCorteCapaSuperior()
    {
        OcultarCorteCapaIntermedia();
        corteCapaSuperior.SetActive(true);
        planoSuperior.SetActive(true);
    }

    public void OcultarCorteCapaSuperior()
    {
        corteCapaSuperior.SetActive(false);
        planoSuperior.SetActive(false);
    }

    public void MostrarCorteCapaIntermedia()
    {
        OcultarCorteCapaSuperior();
        corteCapaIntermedia.SetActive(true);
        planoIntermedio.SetActive(true);
    }

    public void OcultarCorteCapaIntermedia()
    {
        corteCapaIntermedia.SetActive(false);
        planoIntermedio.SetActive(false);
    }
}
