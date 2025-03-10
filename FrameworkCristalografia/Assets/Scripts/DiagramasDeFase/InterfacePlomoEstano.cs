using UnityEngine;
using TMPro;
using System;

public class InterfacePlomoEstano : MonoBehaviour
{
    // Grilla regla de la palanca
    public TMP_Text textoSCLiquida, textoSLLiquida, textoSCAlpha, textoSLAlpha, textoCLBeta, textoSLBeta;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha, resultadoFaseBeta;
    
    // Grilla resultados
    public TMP_Text porcentajeFL, porcentajeEstanoFL, porcentajePlomoFL;
    public TMP_Text porcentajeFA, porcentajeEstanoFA, porcentajePlomoFA;
    public TMP_Text porcentajeFB, porcentajeEstanoFB, porcentajePlomoFB;

    public void ActualizarResultados(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeFaseBeta, float porcentajeEstano, float porcentajePlomo, float L, float C, float S)
    {
        Restablecer();

        porcentajeFL.text = $"{porcentajeFaseLiquida}%";
        porcentajeFA.text = $"{porcentajeFaseAlpha}%";
        porcentajeFB.text = $"{porcentajeFaseBeta}%";

        if (porcentajeFaseLiquida == 100f) // Zona L
        {
            porcentajeEstanoFL.text = $"{porcentajeEstano}%";
            porcentajePlomoFL.text = $"{porcentajePlomo}%";
        }
        else if (porcentajeFaseAlpha == 100f) // Zona Alpha
        {
            porcentajeEstanoFA.text = $"{porcentajeEstano}%";
            porcentajePlomoFA.text = $"{porcentajePlomo}%";
        }
        else if (porcentajeFaseBeta == 100f) // Zona beta
        {
            porcentajeEstanoFB.text = $"{porcentajeEstano}%";
            porcentajePlomoFB.text = $"{porcentajePlomo}%";
        }
        else if (porcentajeFaseLiquida > 0 && porcentajeFaseAlpha > 0) // Zona L + Alpha
        {
            resultadoFaseL.text = $"{porcentajeFaseLiquida}%";
            resultadoFaseAlpha.text = $"{porcentajeFaseAlpha}%";

            porcentajeEstanoFL.text = S + "%";
            porcentajePlomoFL.text = Math.Round(100f-S, 2) + "%";
            
            porcentajeEstanoFA.text = L + "%";
            porcentajePlomoFA.text = Math.Round(100f-L, 2) + "%";

            textoSCLiquida.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            textoSLLiquida.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            textoSCAlpha.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
            textoSLAlpha.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
        else if (porcentajeFaseLiquida > 0 && porcentajeFaseBeta > 0) // Zona L + Beta
        {
            resultadoFaseL.text = $"{porcentajeFaseLiquida}%";
            resultadoFaseBeta.text = $"{porcentajeFaseBeta}%";

            porcentajeEstanoFL.text = L + "%";
            porcentajePlomoFL.text = Math.Round(100f-L, 2) + "%";
            
            porcentajeEstanoFB.text = S + "%";
            porcentajePlomoFB.text = Math.Round(100f-S, 2) + "%";

            textoSCLiquida.text = $"<color=blue>{S}</color> - <color=red>{C}</color>";
            textoSLLiquida.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            textoCLBeta.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            textoSLBeta.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
        else // Zona Alpha + Beta
        {
            resultadoFaseAlpha.text = $"{porcentajeFaseAlpha}%";
            resultadoFaseBeta.text = $"{porcentajeFaseBeta}%";

            porcentajeEstanoFA.text = L + "%";
            porcentajePlomoFA.text = Math.Round(100f-L, 2) + "%";

            porcentajeEstanoFB.text = S + "%";
            porcentajePlomoFB.text = Math.Round(100f-S, 2) + "%";  
            
            textoSCAlpha.text = $"<color=blue>{S}</color> - <color=red><color=red>{C}</color></color>";
            textoSLAlpha.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";

            textoCLBeta.text = $"<color=red>{C}</color> - <color=#14a700>{L}</color>";
            textoSLBeta.text = $"<color=blue>{S}</color> - <color=#14a700>{L}</color>";
        }
    }

    public void Restablecer()
    {
        textoSCLiquida.text = "";
        textoSLLiquida.text = "";
        resultadoFaseL.text = "0%";

        textoSCAlpha.text = "";
        textoSLAlpha.text = ""; 
        resultadoFaseAlpha.text = "0%";

        textoCLBeta.text = "";
        textoSLBeta.text = ""; 
        resultadoFaseBeta.text = "0%";

        porcentajeFL.text = "0%";
        porcentajeEstanoFL.text = "0%";
        porcentajePlomoFL.text = "0%";

        porcentajeFA.text = "0%";
        porcentajeEstanoFA.text = "0%";
        porcentajePlomoFA.text = "0%";

        porcentajeFB.text = "0%";
        porcentajeEstanoFB.text = "0%";
        porcentajePlomoFB.text = "0%";
    }
}
