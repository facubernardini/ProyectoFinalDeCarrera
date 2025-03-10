using UnityEngine;
using TMPro;
using System;

public class InterfaceCobreNiquel : MonoBehaviour
{
    // Grilla regla de la palanca
    public TMP_Text textoSC, textoSL1, textoSL2, textoCL;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha;

    // Grilla resultados
    public TMP_Text porcentajeFaseLiquida, porcentajeNiquelFL, porcentajeCobreFL;
    public TMP_Text porcentajeFaseSolida, porcentajeNiquelFS, porcentajeCobreFS;

    void Start()
    {
        Restablecer();
    }

    public void ActualizarResultados(float porcentajeFL, float porcentajeFS, float porcentajeNiquel, float porcentajeCobre, float L, float C, float S)
    {
        Restablecer();

        porcentajeFaseLiquida.text = $"{porcentajeFL}%";
        porcentajeFaseSolida.text = $"{porcentajeFS}%";

        if (porcentajeFL == 100f) // Zona liquida
        {
            porcentajeCobreFL.text = $"{porcentajeCobre}%";
            porcentajeNiquelFL.text = $"{porcentajeNiquel}%";
        }
        else if (porcentajeFS == 100f) // Zona solida
        {
            porcentajeCobreFS.text = $"{porcentajeCobre}%";
            porcentajeNiquelFS.text = $"{porcentajeNiquel}%";
        }
        else // Zona liquida + alpha
        {
            resultadoFaseL.text = $"{porcentajeFL}%";
            resultadoFaseAlpha.text = $"{porcentajeFS}%";

            porcentajeNiquelFL.text = L + "%";
            porcentajeCobreFL.text = Math.Round(100f-L, 2) + "%";
            
            porcentajeNiquelFS.text = S + "%";
            porcentajeCobreFS.text = Math.Round(100f-S, 2) + "%";
            

            textoSC.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
            textoSL1.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            textoCL.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            textoSL2.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
        
    }

    public void Restablecer()
    {
        textoSC.text = "(  <color=blue>S</color>  -  <color=red>C</color>  )";
        textoSL1.text = "(  <color=blue>S</color>  -  <color=#14a700>L</color>  )";
        resultadoFaseL.text = "0%";

        textoCL.text = "(  <color=red>C</color>  -  <color=#14a700>L</color>  )";
        textoSL2.text = "(  <color=blue>S</color>  -  <color=#14a700>L</color>  )"; 
        resultadoFaseAlpha.text = "0%";

        porcentajeFaseLiquida.text = "0%";
        porcentajeNiquelFL.text = "0%";
        porcentajeCobreFL.text = "0%";

        porcentajeFaseSolida.text = "0%";
        porcentajeNiquelFS.text = "0%";
        porcentajeCobreFS.text = "0%";
    }

}