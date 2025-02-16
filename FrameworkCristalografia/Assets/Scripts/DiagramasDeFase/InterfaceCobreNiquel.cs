using UnityEngine;
using TMPro;

public class InterfaceCobreNiquel : MonoBehaviour
{
    public TMP_Text textSC, textSL1, textSL2, textCL;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha;
    public TMP_Text porcentajeFaseLiquida, porcentajeNiquelFL, porcentajeCobreFL;
    public TMP_Text porcentajeFaseSolida, porcentajeNiquelFS, porcentajeCobreFS;
/*
    public void ActualizarResultadosZonaLiquida(float porcentajeNiquel, float porcentajeCobre)
    {
        resultadoFaseL.text = "= 100%";
        resultadoFaseAlpha.text = "= 0%";

        porcentajeFaseLiquida.text = "100%";
        porcentajeCobreFL.text = porcentajeCobre + "%";
        porcentajeNiquelFL.text = porcentajeNiquel + "%";

        porcentajeFaseSolida.text = "0%";
        porcentajeCobreFS.text = "0%";
        porcentajeNiquelFS.text = "0%";
    }

    public void ActualizarResultadosZonaSolida(float porcentajeNiquel, float porcentajeCobre)
    {
        resultadoFaseL.text = "= 0%";
        resultadoFaseAlpha.text = "= 100%";

        porcentajeFaseLiquida.text = "0%";
        porcentajeCobreFL.text = "0%";
        porcentajeNiquelFL.text = "0%";

        porcentajeFaseSolida.text = "100%";
        porcentajeCobreFS.text = porcentajeCobre + "%";
        porcentajeNiquelFS.text = porcentajeNiquel + "%";
    }
*/
    public void ActualizarResultados(float porcentajeFL, float porcentajeFS, float porcentajeNiquel, float porcentajeCobre, float L, float C, float S)
    {
        Restablecer();

        resultadoFaseL.text = "= " + porcentajeFL + "%";
        resultadoFaseAlpha.text = "= " + porcentajeFS + "%";

        porcentajeFaseLiquida.text = porcentajeFL + "%";
        porcentajeFaseSolida.text = porcentajeFS + "%";

        if (porcentajeFL == 100f) // Zona liquida
        {
            porcentajeCobreFL.text = porcentajeCobre + "%";
            porcentajeNiquelFL.text = porcentajeNiquel + "%";

            porcentajeCobreFS.text = "0%";
            porcentajeNiquelFS.text = "0%";
        }
        else if (porcentajeFS == 100f) // Zona solida
        {
            porcentajeCobreFL.text = "0%";
            porcentajeNiquelFL.text = "0%";

            porcentajeCobreFS.text = porcentajeCobre + "%";
            porcentajeNiquelFS.text = porcentajeNiquel + "%";
        }
        else // Zona liquida + alpha
        {
            porcentajeCobreFL.text = 100-L + "%";
            porcentajeNiquelFL.text = L + "%";

            porcentajeCobreFS.text = 100-S + "%";
            porcentajeNiquelFS.text = S + "%";
        }
        
    }

    public void Restablecer()
    {
        textSC.text = "(S-C)";
        textSL1.text = "(S-L)";
        resultadoFaseL.text = "=  -";

        textCL.text = "(C-L)";
        textSL2.text = "(S-L)"; 
        resultadoFaseAlpha.text = "=  -";

        porcentajeFaseLiquida.text = "-";
        porcentajeNiquelFL.text = "-";
        porcentajeCobreFL.text = "-";

        porcentajeFaseSolida.text = "-";
        porcentajeNiquelFS.text = "-";
        porcentajeCobreFS.text = "-";
    }
}
