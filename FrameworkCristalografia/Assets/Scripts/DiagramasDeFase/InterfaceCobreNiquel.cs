using UnityEngine;
using TMPro;
using System;

public class InterfaceCobreNiquel : MonoBehaviour
{
    public TMP_Text textoSC, textoSL1, textoSL2, textoCL;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha;
    public TMP_Text porcentajeFaseLiquida, porcentajeNiquelFL, porcentajeCobreFL;
    public TMP_Text porcentajeFaseSolida, porcentajeNiquelFS, porcentajeCobreFS;

    public void ActualizarResultados(float porcentajeFL, float porcentajeFS, float porcentajeNiquel, float porcentajeCobre, float L, float C, float S)
    {
        Restablecer();

        resultadoFaseL.text = $"= {porcentajeFL}%";
        resultadoFaseAlpha.text = $"= {porcentajeFS}%";

        porcentajeFaseLiquida.text = $"{porcentajeFL}%";
        porcentajeFaseSolida.text = $"{porcentajeFS}%";

        if (porcentajeFL == 100f) // Zona liquida
        {
            porcentajeCobreFL.text = $"{porcentajeCobre}%";
            porcentajeNiquelFL.text = $"{porcentajeNiquel}%";

            porcentajeCobreFS.text = "0%";
            porcentajeNiquelFS.text = "0%";
        }
        else if (porcentajeFS == 100f) // Zona solida
        {
            porcentajeCobreFL.text = "0%";
            porcentajeNiquelFL.text = "0%";

            porcentajeCobreFS.text = $"{porcentajeCobre}%";
            porcentajeNiquelFS.text = $"{porcentajeNiquel}%";
        }
        else // Zona liquida + alpha
        {
            porcentajeNiquelFL.text = L + "%";
            porcentajeCobreFL.text = Math.Round(100f-L, 2) + "%";
            
            porcentajeNiquelFS.text = S + "%";
            porcentajeCobreFS.text = Math.Round(100f-S, 2) + "%";
            

            textoSC.text = $"({S} - {C})";
            textoSL1.text = $"({S} - {L})";

            textoCL.text = $"({C} - {L})";
            textoSL2.text = $"({S} - {L})";
        }
        
    }

    public void Restablecer()
    {
        textoSC.text = "(  S  -  C  )";
        textoSL1.text = "(  S  -  L  )";
        resultadoFaseL.text = "=  0%";

        textoCL.text = "(  C  -  L  )";
        textoSL2.text = "(  S  -  L  )"; 
        resultadoFaseAlpha.text = "=  0%";

        porcentajeFaseLiquida.text = "0%";
        porcentajeNiquelFL.text = "0%";
        porcentajeCobreFL.text = "0%";

        porcentajeFaseSolida.text = "0%";
        porcentajeNiquelFS.text = "0%";
        porcentajeCobreFS.text = "0%";
    }

}