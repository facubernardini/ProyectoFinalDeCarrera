using UnityEngine;
using UnityEngine.UI;

public class GeneradorEsferasProporciones : MonoBehaviour
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
        EstablecerRadiosAtomicos();
    }

    public void SimulacionProporciones()
    {
        filaActual = minY;
        columnaActual = minX;

        VaciarContenedor();
        AsignarSeparacionEntreEsferas();
        InvokeRepeating("IniciarSimulacionProporciones", 0f, 0.02f);
    }

    private void VaciarContenedor()
    {     
        foreach (Transform bola in contenedorBolas)
        {
            Destroy(bola.gameObject);
        }
    }

    private void AsignarSeparacionEntreEsferas()
    {
        separacion = 0;

        if (sliderMolibdeno.interactable && (sliderMolibdeno.value > 0))
        {
            separacion = 0.14f * radioMolibdeno;
        }
        else if (sliderVanadio.interactable && (sliderVanadio.value > 0))
        {
            separacion = 0.14f * radioVanadio;
        }
        else if (sliderCromo.interactable && (sliderCromo.value > 0))
        {
            separacion = 0.14f * radioCromo;
        }
        else if (sliderHierro.value > 0)
        {
            separacion = 0.14f * radioHierro;
        }
        else if (sliderNiquel.interactable && (sliderNiquel.value > 0))
        {
            separacion = 0.14f * radioNiquel;
        }
        else if (sliderCarbono.interactable && (sliderCarbono.value > 0))
        {
            separacion = 0.14f * radioCarbono;
        }
    }
    
    private void IniciarSimulacionProporciones()
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
        else
        {
            CancelInvoke("IniciarSimulacionProporciones");
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