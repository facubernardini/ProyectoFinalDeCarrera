using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInterfaceDF : MonoBehaviour
{
    public GameObject prefabPuntoL, prefabPuntoC, prefabPuntoS;
    public TMP_InputField inputTemp, inputPorcentajeMat1, inputPorcentajeMat2;
    public TMP_Text textoTempMin, textoTempMax, textoPorcentajeMat1, textoPorcentajeMat2;
    public Slider sliderTemp, sliderPorcentMat1, sliderPorcentMat2;
    public TMP_Text textZona;
    public InterfaceCobreNiquel managerInterfaceCuNi;
    public InterfacePlomoEstano managerInterfacePbSn;
    private GameObject puntoL, puntoC, puntoS;
    private bool modoCuNi, modoPbSn, modoFeC;

    void Start()
    {
        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;
    }
    public void ModoCobreNiquel()
    {
        sliderTemp.minValue = 1000f;
        sliderTemp.maxValue = 1500f;

        textoTempMin.text = "1000°C";
        textoTempMax.text = "1500°C";
        textoPorcentajeMat1.text = "Porcentaje Ni:";
        textoPorcentajeMat2.text = "Porcentaje Cu:";

        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;
    }

    public void ModoPlomoEstano()
    {
        sliderTemp.minValue = 0f;
        sliderTemp.maxValue = 400f;

        textoTempMin.text = "0°C";
        textoTempMax.text = "400°C";
        textoPorcentajeMat1.text = "Porcentaje Sn:";
        textoPorcentajeMat2.text = "Porcentaje Pb:";

        modoCuNi = false;
        modoPbSn = true;
        modoFeC = false;
    }

    public void ModoHierroCarbono()
    {
        sliderTemp.minValue = 400f;
        sliderTemp.maxValue = 1600f;

        textoTempMin.text = "400°C";
        textoTempMax.text = "1600°C";
        textoPorcentajeMat1.text = "Porcentaje C:";
        textoPorcentajeMat2.text = "Porcentaje Fe:";

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
        inputPorcentajeMat1.text = sliderPorcentMat1.value + "";
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

    public void Restablecer()
    {
        DestruirPuntos();
        ActualizarPorcentajeFaseUno(0);
        ActualizarPorcentajeFaseDos(100);
        ActualizarZona("");

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
            //managerInterfaceFeC.Restablecer();
        }
        
    }

    public void ColocarPuntos(Vector3 ubicacionPuntoL, Vector3 ubicacionPuntoC, Vector3 ubicacionPuntoS)
    {
        DestruirPuntos();
        
        puntoL = Instantiate(prefabPuntoL, ubicacionPuntoL, Quaternion.identity);
        puntoC = Instantiate(prefabPuntoC, ubicacionPuntoC, Quaternion.identity);
        puntoS = Instantiate(prefabPuntoS, ubicacionPuntoS, Quaternion.identity);
    }

    public void ColocarPuntoC(Vector3 ubicacionPuntoC)
    {
        DestruirPuntos();
        
        puntoC = Instantiate(prefabPuntoC, ubicacionPuntoC, Quaternion.identity);
    }

    private void DestruirPuntos()
    {
        Destroy(puntoL);
        Destroy(puntoC);
        Destroy(puntoS);
    }
    
}
