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
    }

    public void ComprobarProporcionCarbono()
    {
        if (sliderCarbono.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderCarbono.value;
            if (sliderCarbono.value > cantidadMaximaPermitida)
            {
                sliderCarbono.value = cantidadMaximaPermitida;
            }
        }
    }

    public void ComprobarProporcionNiquel()
    {
        if (sliderNiquel.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderNiquel.value;
            if (sliderNiquel.value > cantidadMaximaPermitida)
            {
                sliderNiquel.value = cantidadMaximaPermitida;
            }
        }
    }

    public void ComprobarProporcionCromo()
    {
        if (sliderCromo.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderCromo.value;
            if (sliderCromo.value > cantidadMaximaPermitida)
            {
                sliderCromo.value = cantidadMaximaPermitida;
            }
        }
    }

    public void ComprobarProporcionMolibdeno()
    {
        if (sliderMolibdeno.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderMolibdeno.value;
            if (sliderMolibdeno.value > cantidadMaximaPermitida)
            {
                sliderMolibdeno.value = cantidadMaximaPermitida;
            }
        }
    }

    public void ComprobarProporcionVanadio()
    {
        if (sliderVanadio.interactable)
        {
            float cantidadMaximaPermitida = 100f - sumaTotal + sliderVanadio.value;
            if (sliderVanadio.value > cantidadMaximaPermitida)
            {
                sliderVanadio.value = cantidadMaximaPermitida;
            }
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
