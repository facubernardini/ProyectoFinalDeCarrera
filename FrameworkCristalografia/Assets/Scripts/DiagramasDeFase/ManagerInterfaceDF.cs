using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInterfaceDF : MonoBehaviour
{
    public GameObject prefabPuntoL, prefabPuntoC, prefabPuntoS;
    public TMP_InputField inputTemp, inputPorcentajeMat1, inputPorcentajeMat2;
    public Slider sliderTemp, sliderPorcentMat1, sliderPorcentMat2;
    public TMP_Text textZona;
    public InterfaceCobreNiquel managerInterfaceCuNi;
    private GameObject puntoL, puntoC, puntoS;

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

    public void ActualizarResultados(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeNiquel, float porcentajeCobre, float L, float C, float S)
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
        ActualizarTemperatura(1000);
        ActualizarPorcentajeFaseUno(0);
        ActualizarPorcentajeFaseDos(100);
        ActualizarZona("");
        managerInterfaceCuNi.Restablecer();
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
