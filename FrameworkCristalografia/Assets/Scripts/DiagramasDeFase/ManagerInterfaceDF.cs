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

    public void ActualizarTemperatura(float temp)
    {
        inputTemp.text = temp + "";
        sliderTemp.value = temp;
    }

    public void ActualizarPorcentajeFaseUno(float porcentaje)
    {
        inputPorcentajeMat1.text = porcentaje + "";
        sliderPorcentMat1.value = porcentaje;
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
        /*if (porcentajeFaseLiquida == 100f)
        {
            managerInterfaceCuNi.ActualizarResultadosZonaLiquida(porcentajeNiquel, porcentajeCobre);
        }
        else if (porcentajeFaseSolida == 100f)
        {
            managerInterfaceCuNi.ActualizarResultadosZonaSolida(porcentajeNiquel, porcentajeCobre);
        }*/
        managerInterfaceCuNi.ActualizarResultados(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeNiquel, porcentajeCobre, L, C, S);
        
    }

    public void Restablecer()
    {
        DestruirPuntos();
        ActualizarTemperatura(0);
        ActualizarPorcentajeFaseUno(0);
        ActualizarPorcentajeFaseDos(0);
        ActualizarZona("-");
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

    public void ReiniciarInterfaz()
    {
        DestruirPuntos();
    }

    private void DestruirPuntos()
    {
        Destroy(puntoL);
        Destroy(puntoC);
        Destroy(puntoS);
    }
    
}
