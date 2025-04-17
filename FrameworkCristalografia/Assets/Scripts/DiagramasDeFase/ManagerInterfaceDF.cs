using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInterfaceDF : MonoBehaviour
{
    public GameObject prefabPuntoL, prefabPuntoC, prefabPuntoS;
    public TMP_InputField inputTemp, inputPorcentajeMat1, inputPorcentajeMat2;
    public TMP_Text textoTempMin, textoTempMax, textoPorcentajeMat1, textoPorcentajeMat2, textoPorcentajeMax;
    public Slider sliderTemp, sliderPorcentMat1, sliderPorcentMat2;
    public TMP_Text textZona;
    public InterfaceCobreNiquel managerInterfaceCuNi;
    public InterfacePlomoEstano managerInterfacePbSn;
    public InterfaceHierroCarbono managerInterfaceFeC;
    public LineRenderer lineRendererTemp, lineRendererL, lineRendererC, lineRendererS;
    public Button restablecer;
    private GameObject puntoL, puntoC, puntoS;
    private bool modoCuNi, modoPbSn, modoFeC;
    private int idioma;

    void Start()
    {
        idioma = PlayerPrefs.GetInt("LocaleKey");

        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;

        ModoCobreNiquel();

        restablecer.interactable = false;
    }

    public void ModoCobreNiquel()
    {
        sliderTemp.minValue = 1000f;
        sliderTemp.maxValue = 1500f;

        sliderPorcentMat1.maxValue = 100f;

        textoTempMin.text = "1000°C";
        textoTempMax.text = "1500°C";
        textoPorcentajeMax.text = "100%";

        switch(idioma) 
        {   
        case 0: // Ingles
            textoPorcentajeMat1.text = "Percentage Ni:";
            textoPorcentajeMat2.text = "Percentage Cu:";
            break;
        case 1: // Frances
            textoPorcentajeMat1.text = "Pourcentage Ni:";
            textoPorcentajeMat2.text = "Pourcentage Cu:";
            break;
        case 2: // Aleman
            textoPorcentajeMat1.text = "Prozentsatz Ni:";
            textoPorcentajeMat2.text = "Prozentsatz Cu:";
            break;
        case 3: // Español
            textoPorcentajeMat1.text = "Porcentaje Ni:";
            textoPorcentajeMat2.text = "Porcentaje Cu:";
            break;
        }

        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;
    }

    public void ModoPlomoEstano()
    {
        sliderTemp.minValue = 0f;
        sliderTemp.maxValue = 400f;

        sliderPorcentMat1.maxValue = 100f;

        textoTempMin.text = "0°C";
        textoTempMax.text = "400°C";
        textoPorcentajeMax.text = "100%";

        switch (idioma) 
        {   
        case 0: // Ingles
            textoPorcentajeMat1.text = "Percentage Sn:";
            textoPorcentajeMat2.text = "Percentage Pb:";
            break;
        case 1: // Frances
            textoPorcentajeMat1.text = "Pourcentage Sn:";
            textoPorcentajeMat2.text = "Pourcentage Pb:";
            break;
        case 2: // Aleman
            textoPorcentajeMat1.text = "Prozentsatz Sn:";
            textoPorcentajeMat2.text = "Prozentsatz Pb:";
            break;
        case 3: // Español
            textoPorcentajeMat1.text = "Porcentaje Sn:";
            textoPorcentajeMat2.text = "Porcentaje Pb:";
            break;
        }

        modoCuNi = false;
        modoPbSn = true;
        modoFeC = false;
    }

    public void ModoHierroCarbono()
    {
        sliderTemp.minValue = 400f;
        sliderTemp.maxValue = 1400f;

        sliderPorcentMat1.maxValue = 6.7f;

        textoTempMin.text = "400°C";
        textoTempMax.text = "1400°C";
        textoPorcentajeMax.text = "6.7%";

        switch(idioma) 
        {   
        case 0: // Ingles
            textoPorcentajeMat1.text = "Percentage C:";
            textoPorcentajeMat2.text = "Percentage Fe:";
            break;
        case 1: // Frances
            textoPorcentajeMat1.text = "Pourcentage C:";
            textoPorcentajeMat2.text = "Pourcentage Fe:";
            break;
        case 2: // Aleman
            textoPorcentajeMat1.text = "Prozentsatz C:";
            textoPorcentajeMat2.text = "Prozentsatz Fe:";
            break;
        case 3: // Español
            textoPorcentajeMat1.text = "Porcentaje C:";
            textoPorcentajeMat2.text = "Porcentaje Fe:";
            break;
        }

        modoCuNi = false;
        modoPbSn = false;
        modoFeC = true;
    }

    public float ObtenerTemperatura()
    {
        return float.Parse(inputTemp.text);
    }

    public float ObtenerPorcentajeFaseUno()
    {
        return float.Parse(inputPorcentajeMat1.text);
    }

    public float ObtenerPorcentajeFaseDos()
    {
        return float.Parse(inputPorcentajeMat2.text);
    }

    public void ActualizarTemperatura(float temp)
    {
        inputTemp.text = temp + "";
        sliderTemp.value = temp;
    }

    public void ActualizarTemperatura()
    {
        inputTemp.text = sliderTemp.value + "";
    }

    public void ActualizarPorcentajeFaseUno(float porcentaje)
    {
        inputPorcentajeMat1.text = porcentaje + "";
        sliderPorcentMat1.value = porcentaje;
    }

    public void ActualizarPorcentajeFaseUno()
    {
        inputPorcentajeMat1.text = (float) Math.Round(sliderPorcentMat1.value, 2) + "";
    }

    public void ActualizarPorcentajeFaseUnoDesdeInput()
    {
        float nuevoPorcentaje = 100f - float.Parse(inputPorcentajeMat2.text);
        inputPorcentajeMat1.text = nuevoPorcentaje + "";
    }

    public void ActualizarPorcentajeFaseDos(float porcentaje)
    {
        inputPorcentajeMat2.text = porcentaje + "";
        sliderPorcentMat2.value = porcentaje;
    }

    public void ActualizarZona(string zona)
    {
        textZona.text = zona;
    }

    public void ActualizarResultadosCobreNiquel(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeNiquel, float porcentajeCobre, float L, float C, float S)
    {
        porcentajeFaseAlpha = (float) Math.Round(porcentajeFaseAlpha, 2);
        porcentajeFaseLiquida = (float) Math.Round(porcentajeFaseLiquida, 2);

        L = (float) Math.Round(L, 2);
        C = (float) Math.Round(C, 2);
        S = (float) Math.Round(S, 2);

        managerInterfaceCuNi.ActualizarResultados(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeNiquel, porcentajeCobre, L, C, S);      
    }

    public void ActualizarResultadosPlomoEstano(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeFaseBeta, float porcentajeEstano, float porcentajePlomo, float L, float C, float S)
    {
        porcentajeFaseAlpha = (float) Math.Round(porcentajeFaseAlpha, 2);
        porcentajeFaseBeta = (float) Math.Round(porcentajeFaseBeta, 2);
        porcentajeFaseLiquida = (float) Math.Round(porcentajeFaseLiquida, 2);

        L = (float) Math.Round(L, 2);
        C = (float) Math.Round(C, 2);
        S = (float) Math.Round(S, 2);

        managerInterfacePbSn.ActualizarResultados(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeFaseBeta, porcentajeEstano, porcentajePlomo, L, C, S);
    }

    public void ActualizarResultadosHierroCarbono(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeFaseGamma, float porcentajeFaseFe3C, float porcentajeCarbono, float porcentajeHierro, float L, float C, float S)
    {
        porcentajeFaseLiquida = (float) Math.Round(porcentajeFaseLiquida, 2);
        porcentajeFaseAlpha = (float) Math.Round(porcentajeFaseAlpha, 2);
        porcentajeFaseGamma = (float) Math.Round(porcentajeFaseGamma, 2);
        porcentajeFaseFe3C = (float) Math.Round(porcentajeFaseFe3C, 2);
        

        L = (float) Math.Round(L, 2);
        C = (float) Math.Round(C, 2);
        S = (float) Math.Round(S, 2);

        managerInterfaceFeC.ActualizarResultados(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeFaseGamma, porcentajeFaseFe3C, porcentajeCarbono, porcentajeHierro, L, C, S);
    }

    public void Restablecer()
    {
        DestruirPuntos();
        ActualizarPorcentajeFaseUno(0);
        ActualizarPorcentajeFaseDos(100);
        ActualizarZona("");
        BorrarLineas();
        restablecer.interactable = false;

        if (modoCuNi)
        {   
            ActualizarTemperatura(1000);
            managerInterfaceCuNi.Restablecer();
        }
        if (modoPbSn)
        {
            ActualizarTemperatura(0);
            managerInterfacePbSn.Restablecer();
        }
        if (modoFeC)
        {
            ActualizarTemperatura(400);
            managerInterfaceFeC.Restablecer();
        }
        
    }

    public void ColocarPuntos(Vector3 ubicacionPuntoL, Vector3 ubicacionPuntoC, Vector3 ubicacionPuntoS)
    {
        DestruirPuntos();
        
        puntoL = Instantiate(prefabPuntoL, ubicacionPuntoL, Quaternion.identity);
        puntoC = Instantiate(prefabPuntoC, ubicacionPuntoC, Quaternion.identity);
        puntoS = Instantiate(prefabPuntoS, ubicacionPuntoS, Quaternion.identity);

        TrazarLineas(ubicacionPuntoL, ubicacionPuntoC, ubicacionPuntoS);

        restablecer.interactable = true;
    }

    public void ColocarPuntoC(Vector3 ubicacionPuntoC)
    {
        DestruirPuntos();
        
        puntoC = Instantiate(prefabPuntoC, ubicacionPuntoC, Quaternion.identity);

        TrazarLineas(ubicacionPuntoC);

        restablecer.interactable = true;
    }

    public void ActualizarColorSliderTemp()
    {
        Color startColor = Color.blue;
        Color endColor = Color.red;

        float t = (sliderTemp.value-sliderTemp.minValue) / (sliderTemp.maxValue-sliderTemp.minValue);
        sliderTemp.fillRect.GetComponent<Image>().color = Color.Lerp(startColor, endColor, t);
    }

    private void DestruirPuntos()
    {
        Destroy(puntoL);
        Destroy(puntoC);
        Destroy(puntoS);
    }

    private void TrazarLineas(Vector3 ubicacionPuntoL, Vector3 ubicacionPuntoC, Vector3 ubicacionPuntoS)
    {
        lineRendererTemp.positionCount = 2;
        lineRendererL.positionCount = 2;
        lineRendererC.positionCount = 2;
        lineRendererS.positionCount = 2;

        lineRendererTemp.SetPosition(0, new Vector3(0f, -0.01f, ubicacionPuntoC.z));
        lineRendererTemp.SetPosition(1, new Vector3(ubicacionPuntoS.x, -0.01f, ubicacionPuntoC.z));

        lineRendererL.SetPosition(0, new Vector3(ubicacionPuntoL.x, -0.01f, ubicacionPuntoL.z));
        lineRendererL.SetPosition(1, new Vector3(ubicacionPuntoL.x, -0.01f, 0f));

        lineRendererC.SetPosition(0, new Vector3(ubicacionPuntoC.x, -0.01f, ubicacionPuntoC.z));
        lineRendererC.SetPosition(1, new Vector3(ubicacionPuntoC.x, -0.01f, 0f));

        lineRendererS.SetPosition(0, new Vector3(ubicacionPuntoS.x, -0.01f, ubicacionPuntoS.z));
        lineRendererS.SetPosition(1, new Vector3(ubicacionPuntoS.x, -0.01f, 0f));
    }

    private void TrazarLineas(Vector3 ubicacionPuntoC)
    {
        BorrarLineas();
        lineRendererTemp.positionCount = 2;
        lineRendererC.positionCount = 2;

        lineRendererTemp.SetPosition(0, new Vector3(0f, -0.01f, ubicacionPuntoC.z));
        lineRendererTemp.SetPosition(1, new Vector3(ubicacionPuntoC.x, -0.01f, ubicacionPuntoC.z));

        lineRendererC.SetPosition(0, new Vector3(ubicacionPuntoC.x, -0.01f, ubicacionPuntoC.z));
        lineRendererC.SetPosition(1, new Vector3(ubicacionPuntoC.x, -0.01f, 0f));
    }

    private void BorrarLineas()
    {
        lineRendererTemp.positionCount = 0;
        lineRendererL.positionCount = 0;
        lineRendererC.positionCount = 0;
        lineRendererS.positionCount = 0;
    }
    
}