using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InterfaceHierroCarbono : MonoBehaviour
{
    // Grilla regla de la palanca
    public TMP_Text numeradorLiquida, denominadorLiquida, numeradorAlpha, denominadorAlpha, numeradorGamma, denominadorGamma, numeradorFe3C, denominadorFe3C;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha, resultadoFaseGamma, resultadoFaseFe3C;

    // Grilla resultados
    public TMP_Text porcentajeFL, porcentajeCarbonoFL, porcentajeHierroFL;
    public TMP_Text porcentajeAlpha, porcentajeCarbonoAlpha, porcentajeHierroAlpha;
    public TMP_Text porcentajeGamma, porcentajeCarbonoGamma, porcentajeHierroGamma;
    public TMP_Text porcentajeFe3C, porcentajeCarbonoFe3C, porcentajeHierroFe3C;
    public Slider temperatura;

    public void ActualizarResultados(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeFaseGamma, float porcentajeFaseFe3C, float porcentajeCarbono, float porcentajeHierro, float L, float C, float S)
    {
        Restablecer();

        porcentajeFL.text = $"{porcentajeFaseLiquida}%";
        porcentajeAlpha.text = $"{porcentajeFaseAlpha}%";
        porcentajeGamma.text = $"{porcentajeFaseGamma}%";
        porcentajeFe3C.text = $"{porcentajeFaseFe3C}%";

        if (porcentajeFaseLiquida == 100f) // Zona L
        {
            porcentajeCarbonoFL.text = $"{porcentajeCarbono}%";
            porcentajeHierroFL.text = $"{porcentajeHierro}%";
        }
        else if (porcentajeFaseAlpha == 100f) // Zona Alpha
        {
            porcentajeCarbonoAlpha.text = $"{porcentajeCarbono}%";
            porcentajeHierroAlpha.text = $"{porcentajeHierro}%";
        }
        else if (porcentajeFaseGamma == 100f) // Zona Gamma
        {
            porcentajeCarbonoGamma.text = $"{porcentajeCarbono}%";
            porcentajeHierroGamma.text = $"{porcentajeHierro}%";
        }
        else if (porcentajeFaseLiquida > 0 && porcentajeFaseGamma > 0) // Zona L+Gamma
        {
            resultadoFaseL.text = $"{porcentajeFaseLiquida}%";
            resultadoFaseGamma.text = $"{porcentajeFaseGamma}%";

            porcentajeCarbonoFL.text = S + "%";
            porcentajeHierroFL.text = Math.Round(100f-S, 2) + "%";
            
            porcentajeCarbonoGamma.text = L + "%";
            porcentajeHierroGamma.text = Math.Round(100f-L, 2) + "%";

            numeradorLiquida.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            denominadorLiquida.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            numeradorGamma.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
            denominadorGamma.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
        else if (porcentajeFaseAlpha > 0 && porcentajeFaseGamma > 0) // Zona Alpha+Gamma
        {
            resultadoFaseAlpha.text = $"{porcentajeFaseAlpha}%";
            resultadoFaseGamma.text = $"{porcentajeFaseGamma}%";

            porcentajeCarbonoGamma.text = S + "%";
            porcentajeHierroGamma.text = Math.Round(100f-S, 2) + "%";
            
            porcentajeCarbonoAlpha.text = L + "%";
            porcentajeHierroAlpha.text = Math.Round(100f-L, 2) + "%";

            numeradorGamma.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            denominadorGamma.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            numeradorAlpha.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
            denominadorAlpha.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
        else 
        {
            if (temperatura.value >= 400 && temperatura.value <= 727) // Zona Alpha+Fe3C
            {
                resultadoFaseAlpha.text = $"{porcentajeFaseAlpha}%";
                resultadoFaseFe3C.text = $"{porcentajeFaseFe3C}%";

                porcentajeCarbonoFe3C.text = S + "%";
                porcentajeHierroFe3C.text = Math.Round(100f-S, 2) + "%";
                
                porcentajeCarbonoAlpha.text = L + "%";
                porcentajeHierroAlpha.text = Math.Round(100f-L, 2) + "%";

                numeradorFe3C.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
                denominadorFe3C.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

                numeradorAlpha.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
                denominadorAlpha.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
            } 
            else if (temperatura.value > 727 && temperatura.value <= 1148) // Zona Gamma+Fe3C
            {
                resultadoFaseGamma.text = $"{porcentajeFaseGamma}%";
                resultadoFaseFe3C.text = $"{porcentajeFaseFe3C}%";

                porcentajeCarbonoFe3C.text = S + "%";
                porcentajeHierroFe3C.text = Math.Round(100f-S, 2) + "%";
                
                porcentajeCarbonoGamma.text = L + "%";
                porcentajeHierroGamma.text = Math.Round(100f-L, 2) + "%";

                numeradorFe3C.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
                denominadorFe3C.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

                numeradorGamma.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
                denominadorGamma.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
            }
            else if (temperatura.value > 1148 && temperatura.value <= 1227) // Zona L+F3C
            {
                resultadoFaseL.text = $"{porcentajeFaseLiquida}%";
                resultadoFaseFe3C.text = $"{porcentajeFaseFe3C}%";

                porcentajeCarbonoFL.text = L + "%";
                porcentajeHierroFL.text = Math.Round(100f-L, 2) + "%";
                
                porcentajeCarbonoFe3C.text = S + "%";
                porcentajeHierroFe3C.text = Math.Round(100f-S, 2) + "%";

                numeradorLiquida.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
                denominadorLiquida.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

                numeradorFe3C.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
                denominadorFe3C.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
            } 
        }
    }

    public void Restablecer()
    {
        numeradorLiquida.text = "";
        denominadorLiquida.text = "";
        resultadoFaseL.text = "0%";

        numeradorAlpha.text = "";
        denominadorAlpha.text = ""; 
        resultadoFaseAlpha.text = "0%";

        numeradorGamma.text = "";
        denominadorGamma.text = ""; 
        resultadoFaseGamma.text = "0%";

        numeradorFe3C.text = "";
        denominadorFe3C.text = ""; 
        resultadoFaseFe3C.text = "0%";

        porcentajeFL.text = "0%";
        porcentajeCarbonoFL.text = "0%";
        porcentajeHierroFL.text = "0%";

        porcentajeAlpha.text = "0%";
        porcentajeCarbonoAlpha.text = "0%";
        porcentajeHierroAlpha.text = "0%";

        porcentajeGamma.text = "0%";
        porcentajeCarbonoGamma.text = "0%";
        porcentajeHierroGamma.text = "0%";

        porcentajeFe3C.text = "0%";
        porcentajeCarbonoFe3C.text = "0%";
        porcentajeHierroFe3C.text = "0%";
    }
}