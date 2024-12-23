using System;
using UnityEngine;
using UnityEngine.UI;

public class LogicaProporciones : MonoBehaviour
{
    public Slider sliderCarbono, sliderNiquel, sliderCromo, sliderMolibdeno, sliderVanadio, sliderHierro;
    private float sumaTotal;

    void Update()
    {
        SumarProporciones();

        CompletarProporcionesConHierro();
        ComprobarProporcionCarbono();
        ComprobarProporcionNiquel();
        ComprobarProporcionCromo();
        ComprobarProporcionMolibdeno();
        ComprobarProporcionVanadio();
    }

    private void ComprobarProporcionCarbono()
    {
        if (sliderCarbono.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderCarbono.value;
            sliderCarbono.value = Math.Clamp(sliderCarbono.value, 0, cantidadMaximaPermitida);
        }
    }

    private void ComprobarProporcionNiquel()
    {
        if (sliderNiquel.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderNiquel.value;
            sliderNiquel.value = Math.Clamp(sliderNiquel.value, 0, cantidadMaximaPermitida);
        }
    }

    private void ComprobarProporcionCromo()
    {
        if (sliderCromo.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderCromo.value;
            sliderCromo.value = Math.Clamp(sliderCromo.value, 0, cantidadMaximaPermitida);
        }
    }

    private void ComprobarProporcionMolibdeno()
    {
        if (sliderMolibdeno.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderMolibdeno.value;
            sliderMolibdeno.value = Math.Clamp(sliderMolibdeno.value, 0, cantidadMaximaPermitida);
        }
    }

    private void ComprobarProporcionVanadio()
    {
        if (sliderVanadio.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderVanadio.value;
            sliderVanadio.value = Math.Clamp(sliderVanadio.value, 0, cantidadMaximaPermitida);
        }
    }

    private void CompletarProporcionesConHierro()
    {
        sliderHierro.value = 100f - sumaTotal;
    }

    private void SumarProporciones()
    {
        float sumaParcial = 0;

        if (sliderCarbono.interactable)
            sumaParcial += sliderCarbono.value;

        if (sliderNiquel.interactable)
            sumaParcial += sliderNiquel.value;

        if (sliderCromo.interactable)
            sumaParcial += sliderCromo.value;

        if (sliderMolibdeno.interactable)
            sumaParcial += sliderMolibdeno.value;

        if (sliderVanadio.interactable)
            sumaParcial += sliderVanadio.value;

        sumaTotal = sumaParcial;
    }
}
