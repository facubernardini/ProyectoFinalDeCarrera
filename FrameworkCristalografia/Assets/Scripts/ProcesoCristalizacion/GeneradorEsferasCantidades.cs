using TMPro;
using UnityEngine;

public class GeneradorEsferasCantidades : MonoBehaviour
{
    public GameObject spherePrefab;
    public float minX, maxX, minY, maxY;
    public Transform contenedorBolas;
    public TMP_InputField inputCarbono, inputNiquel, inputCromo, inputMolibdeno, inputVanadio, inputHierro;

    private float filaActual, columnaActual, separacion;
    private float radioCarbono, radioNiquel, radioCromo, radioMolibdeno, radioVanadio, radioHierro;
    private float proporcionCarbono, proporcionNiquel, proporcionCromo, proporcionMolibdeno, proporcionVanadio, proporcionHierro;
    private int cantidadEsferas;
    private int carbonoGenerado, niquelGenerado, cromoGenerado, molibdenoGenerado, vanadioGenerado, hierroGenerado;
    private Color colorEsfera;
    
    void Start()
    { 
        EstablecerRadiosAtomicos();
    }

    public void SimulacionCantidades()
    {
        filaActual = minY;
        columnaActual = minX;

        VaciarContenedor();
        AsignarSeparacionEntreEsferas();
        CalcularTotalEsferas();
        CalcularProporciones();
        InvokeRepeating("IniciarSimulacionCantidades", 0f, 0.02f);
    }

    private void VaciarContenedor()
    {
        foreach (Transform bola in contenedorBolas)
        {
            Destroy(bola.gameObject);
        }

        carbonoGenerado = 0;
        niquelGenerado = 0;
        cromoGenerado = 0;
        molibdenoGenerado = 0;
        vanadioGenerado = 0;
        hierroGenerado = 0;
    }

    private void AsignarSeparacionEntreEsferas()
    {
        separacion = 0;

        if (inputMolibdeno.interactable && int.Parse(inputMolibdeno.text) > 0)
        {
            separacion = 0.14f * radioMolibdeno;
        }
        else if (inputVanadio.interactable && int.Parse(inputVanadio.text) > 0)
        {
            separacion = 0.14f * radioVanadio;
        }
        else if (inputCromo.interactable && int.Parse(inputCromo.text) > 0)
        {
            separacion = 0.14f * radioCromo;
        }
        else if (inputHierro.interactable && int.Parse(inputHierro.text) > 0)
        {
            separacion = 0.14f * radioHierro;
        }
        else if (inputNiquel.interactable && int.Parse(inputNiquel.text) > 0)
        {
            separacion = 0.14f * radioNiquel;
        }
        else if (inputCarbono.interactable && int.Parse(inputCarbono.text) > 0)
        {
            separacion = 0.14f * radioCarbono;
        }
    }

    private void CalcularTotalEsferas()
    {
        cantidadEsferas = 0;

        cantidadEsferas += int.Parse(inputCarbono.text);
        cantidadEsferas += int.Parse(inputNiquel.text);
        cantidadEsferas += int.Parse(inputCromo.text);
        cantidadEsferas += int.Parse(inputMolibdeno.text);
        cantidadEsferas += int.Parse(inputVanadio.text);
        cantidadEsferas += int.Parse(inputHierro.text);
    }

    private void CalcularProporciones()
    {
        proporcionCarbono = (float.Parse(inputCarbono.text) / cantidadEsferas) * 100;
        proporcionNiquel = (float.Parse(inputNiquel.text) / cantidadEsferas) * 100;
        proporcionCromo = (float.Parse(inputCromo.text) / cantidadEsferas) * 100;
        proporcionMolibdeno = (float.Parse(inputMolibdeno.text) / cantidadEsferas) * 100;
        proporcionVanadio = (float.Parse(inputVanadio.text) / cantidadEsferas) * 100;
        proporcionHierro = (float.Parse(inputHierro.text) / cantidadEsferas) * 100;
    }

    private void IniciarSimulacionCantidades()
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

                if ((carbonoGenerado + cromoGenerado + niquelGenerado + vanadioGenerado + molibdenoGenerado + hierroGenerado) == cantidadEsferas)
                {
                    CancelInvoke("IniciarSimulacionCantidades");
                }
            }

            columnaActual = minX;
            filaActual += separacion;
        }
        else
        {
            CancelInvoke("IniciarSimulacionCantidades");
        }
    }

    private float ObtenerRadioEsferaGenerada()
    {
        int numAleatorio = Random.Range(1, 10000);
        
        float proporcionActual = 0;
        float proporcionAcumulada = 0;
        float radioEsfera = 0;

        if (inputCarbono.interactable && carbonoGenerado < int.Parse(inputCarbono.text))
        {
            proporcionAcumulada += proporcionCarbono * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioCarbono;
                colorEsfera = new Color(210f / 255f, 190f / 255f, 60f / 255f);
                carbonoGenerado++;
            }

            proporcionActual = proporcionAcumulada;
        }

        if (inputNiquel.interactable && niquelGenerado < int.Parse(inputNiquel.text))
        {
            proporcionAcumulada += proporcionNiquel * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioNiquel;
                colorEsfera = new Color(110f / 255f, 210f / 255f, 60f / 255f);
                niquelGenerado++;
            }

            proporcionActual = proporcionAcumulada;
        }

        if (inputCromo.interactable && cromoGenerado < int.Parse(inputCromo.text))
        {
            proporcionAcumulada += proporcionCromo * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioCromo;
                colorEsfera = new Color(0f / 255f, 210f / 255f, 210f / 255f);
                cromoGenerado++;
            }

            proporcionActual = proporcionAcumulada;
        }

        if (inputMolibdeno.interactable && molibdenoGenerado < int.Parse(inputMolibdeno.text))
        {
            proporcionAcumulada += proporcionMolibdeno * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioMolibdeno;
                colorEsfera = new Color(30f / 255f, 100f / 255f, 230f / 255f);
                molibdenoGenerado++;
            }

            proporcionActual = proporcionAcumulada;
        }

        if (inputVanadio.interactable && vanadioGenerado < int.Parse(inputVanadio.text))
        {
            proporcionAcumulada += proporcionVanadio * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioVanadio;
                colorEsfera = new Color(160f / 255f, 60f / 255f, 250f / 255f);
                vanadioGenerado++;
            }

            proporcionActual = proporcionAcumulada;
        }

        if (inputHierro.interactable && hierroGenerado < int.Parse(inputHierro.text))
        {
            proporcionAcumulada += proporcionHierro * 100;

            if (numAleatorio >= proporcionActual && numAleatorio < proporcionAcumulada)
            {
                radioEsfera = radioHierro;
                colorEsfera = new Color(245f / 255f, 90f / 255f, 50f / 255f);
                hierroGenerado++;
            }
        }

        return radioEsfera;
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
}