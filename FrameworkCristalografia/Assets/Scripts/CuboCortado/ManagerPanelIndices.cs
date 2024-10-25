using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelIndices : MonoBehaviour
{
    public Slider sliderX, sliderY, sliderZ;
    public TextMeshProUGUI valorX, valorY, valorZ;
    private Boolean modoContinuoActivado;
    private float umbral;

    void Start()
    {
        modoContinuoActivado = false;
        umbral = 0.16f;

        valorX.text = sliderX.value + "";
        valorY.text = sliderY.value + "";
        valorZ.text = sliderZ.value + "";
    }
    public void ActivarModoContinuo()
    {
        if (!modoContinuoActivado)
        {
            sliderX.wholeNumbers = false;
            sliderY.wholeNumbers = false;
            sliderZ.wholeNumbers = false;
        }
        else
        {
            sliderX.wholeNumbers = true;
            sliderY.wholeNumbers = true;
            sliderZ.wholeNumbers = true;
        }

        modoContinuoActivado = !modoContinuoActivado;
    }

    public void ActualizarValorX()
    {
        valorX.text = Math.Round(sliderX.value, 2) + "";
    }

    public void ActualizarValorY()
    {
        valorY.text = Math.Round(sliderY.value, 2) + "";
    }

    public void ActualizarValorZ()
    {
        valorZ.text = Math.Round(sliderZ.value, 2) + "";
    }

    public void ValorEnteroSlider()
    {
        if (modoContinuoActivado)
        {
            float valorRealX = sliderX.value;
            float valorRealY = sliderY.value;
            float valorRealZ = sliderZ.value;

            int valorRedondeadoX = Mathf.RoundToInt(valorRealX);
            int valorRedondeadoY = Mathf.RoundToInt(valorRealY);
            int valorRedondeadoZ = Mathf.RoundToInt(valorRealZ);

            bool estaCercaX = Mathf.Abs(valorRealX - valorRedondeadoX) < umbral;
            bool estaCercaY = Mathf.Abs(valorRealY - valorRedondeadoY) < umbral;
            bool estaCercaZ = Mathf.Abs(valorRealZ - valorRedondeadoZ) < umbral;

            if (estaCercaX)
            {
                sliderX.value = valorRedondeadoX;
            }

            if (estaCercaY)
            {
                sliderY.value = valorRedondeadoY;
            }

            if (estaCercaZ)
            {
                sliderZ.value = valorRedondeadoZ;
            }
        }
    }
}