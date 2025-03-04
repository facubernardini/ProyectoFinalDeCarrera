using UnityEngine;
using TMPro;
using System;

public class InterfacePlomoEstano : MonoBehaviour
{
    public TMP_Text textoSCLiquida, textoSLLiquida, textoSCAlpha, textoSLAlpha, textoCLBeta, textoSLBeta;
    public TMP_Text resultadoFaseL, resultadoFaseAlpha, resultadoFaseBeta;
    public TMP_Text porcentajeFL, porcentajeEstanoFL, porcentajePlomoFL;
    public TMP_Text porcentajeFA, porcentajeEstanoFA, porcentajePlomoFA;
    public TMP_Text porcentajeFB, porcentajeEstanoFB, porcentajePlomoFB;

    public void ActualizarResultados(float porcentajeFaseLiquida, float porcentajeFaseAlpha, float porcentajeFaseBeta, float porcentajeEstano, float porcentajePlomo, float L, float C, float S)
    {
        Restablecer();

        resultadoFaseL.text = $"= {porcentajeFaseLiquida}%";
        resultadoFaseAlpha.text = $"= {porcentajeFaseAlpha}%";
        resultadoFaseBeta.text = $"= {porcentajeFaseBeta}%";

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
            porcentajeEstanoFL.text = S + "%";
            porcentajePlomoFL.text = Math.Round(100f-S, 2) + "%";
            
            porcentajeEstanoFA.text = L + "%";
            porcentajePlomoFA.text = Math.Round(100f-L, 2) + "%";

            textoSCLiquida.text = $"({C} - {L})";
            textoSLLiquida.text = $"({S} - {L})";

            textoSCAlpha.text = $"({S} - {C})";
            textoSLAlpha.text = $"({S} - {L})";
        }
        else if (porcentajeFaseLiquida > 0 && porcentajeFaseBeta > 0) // Zona L + Beta
        {
            porcentajeEstanoFL.text = L + "%";
            porcentajePlomoFL.text = Math.Round(100f-L, 2) + "%";
            
            porcentajeEstanoFB.text = S + "%";
            porcentajePlomoFB.text = Math.Round(100f-S, 2) + "%";

            textoSCLiquida.text = $"({S} - {C})";
            textoSLLiquida.text = $"({S} - {L})";

            textoCLBeta.text = $"({C} - {L})";
            textoSLBeta.text = $"({S} - {L})";
        }
        else // Zona Alpha + Beta
        {
            porcentajeEstanoFA.text = L + "%";
            porcentajePlomoFA.text = Math.Round(100f-L, 2) + "%";

            porcentajeEstanoFB.text = S + "%";
            porcentajePlomoFB.text = Math.Round(100f-S, 2) + "%";  
            
            textoSCAlpha.text = $"({S} - {C})";
            textoSLAlpha.text = $"({S} - {L})";

            textoCLBeta.text = $"({C} - {L})";
            textoSLBeta.text = $"({S} - {L})";
        }
    }

    public void Restablecer()
    {
        textoSCLiquida.text = "(S-C) | (C-L)";
        textoSLLiquida.text = "(  S  -  L  )";
        resultadoFaseL.text = "=  0%";

        textoSCAlpha.text = "(  S  -  C  )";
        textoSLAlpha.text = "(  S  -  L  )"; 
        resultadoFaseAlpha.text = "=  0%";

        textoCLBeta.text = "(  C  -  L  )";
        textoSLBeta.text = "(  S  -  L  )"; 
        resultadoFaseBeta.text = "=  0%";

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
