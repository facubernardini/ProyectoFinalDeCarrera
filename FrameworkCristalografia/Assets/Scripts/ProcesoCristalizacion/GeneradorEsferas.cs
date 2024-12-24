using UnityEngine;
using UnityEngine.UI;

public class GeneradorEsferas : MonoBehaviour
{
    public GameObject spherePrefab;
    public float minX, maxX, minY, maxY;
    public Transform contenedorBolas;
    public Slider sliderCarbono, sliderNiquel, sliderCromo, sliderMolibdeno, sliderVanadio, sliderHierro;

    private float filaActual, columnaActual, separacion;
    private float radioCarbono, radioNiquel, radioCromo, radioMolibdeno, radioVanadio, radioHierro;
    private Color colorEsfera;

    void Start()
    {
        separacion = 0.15f;

        EstablecerRadiosAtomicos();
    }

    public void GenerarSimulacion()
    {
        filaActual = minY;
        columnaActual = minX;

        VaciarContenedor();
        InvokeRepeating("CrearFila", 0f, 0.02f);
    }
    
    private void CrearFila()
    {
        if (filaActual <= maxY)
        {
            while (columnaActual <= maxX)
            {
                Vector3 newPosition = new Vector3(columnaActual, filaActual, 0);
                GameObject nuevaEsfera = Instantiate(spherePrefab, newPosition, transform.rotation);
                nuevaEsfera.transform.SetParent(contenedorBolas);

                nuevaEsfera.transform.localScale *= ObtenerRadioEsferaGenerada();
                nuevaEsfera.GetComponent<Renderer>().material.color = colorEsfera;
                
                columnaActual += separacion;
            }

            columnaActual = minX;
            filaActual += separacion;
        }
    }

    private void VaciarContenedor()
    {
        CancelInvoke("CrearFila");
        
        foreach (Transform bola in contenedorBolas)
        {
            Destroy(bola.gameObject);
        }
    }

    private void EstablecerRadiosAtomicos()
    {
        radioCarbono = 0.429f;
        radioNiquel = 0.955f;
        radioCromo = 1.064f;
        radioMolibdeno = 1.217f;
        radioVanadio = 1.096f;
        radioHierro = 1f;
    }

    private float ObtenerRadioEsferaGenerada()
    {
        int numAleatorio = Random.Range(1, 10000);
        float proporcionActual = 0;
        float proporcionAcumulada = 0;
        float radioEsfera = 0;

        if (sliderCarbono.interactable)
        {
            proporcionAcumulada += sliderCarbono.value * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioCarbono;
                colorEsfera = new Color(210f / 255f, 190f / 255f, 60f / 255f);
            }

            proporcionActual = proporcionAcumulada;
        }

        if (sliderNiquel.interactable)
        {
            proporcionAcumulada += sliderNiquel.value * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioNiquel;
                colorEsfera = new Color(110f / 255f, 210f / 255f, 60f / 255f);
            }

            proporcionActual = proporcionAcumulada;
        }

        if (sliderCromo.interactable)
        {
            proporcionAcumulada += sliderCromo.value * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioCromo;
                colorEsfera = new Color(0f / 255f, 210f / 255f, 210f / 255f);
            }

            proporcionActual = proporcionAcumulada;
        }

        if (sliderMolibdeno.interactable)
        {
            proporcionAcumulada += sliderMolibdeno.value * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioMolibdeno;
                colorEsfera = new Color(30f / 255f, 100f / 255f, 230f / 255f);
            }

            proporcionActual = proporcionAcumulada;
        }

        if (sliderVanadio.interactable)
        {
            proporcionAcumulada += sliderVanadio.value * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioVanadio;
                colorEsfera = new Color(160f / 255f, 60f / 255f, 250f / 255f);
            }

            proporcionActual = proporcionAcumulada;
        }

        proporcionAcumulada += sliderHierro.value * 100;

        if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
        {
            radioEsfera = radioHierro;
            colorEsfera = new Color(245f / 255f, 90f / 255f, 50f / 255f);
        }

        return radioEsfera;
    }
}