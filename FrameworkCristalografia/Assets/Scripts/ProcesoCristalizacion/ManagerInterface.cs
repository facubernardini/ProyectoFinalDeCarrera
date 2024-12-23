using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerInterface : MonoBehaviour
{
    public Slider sliderCarbono, sliderNiquel, sliderCromo, sliderMolibdeno, sliderVanadio, sliderHierro;
    public TMP_InputField inputCarbono, inputNiquel, inputCromo, inputMolibdeno, inputVanadio, inputHierro;

    void Start()
    {
        ModificarInputCarbono();
        ModificarInputNiquel();
        ModificarInputCromo();
        ModificarInputMolibdeno();
        ModificarInputVanadio();
        ModificarInputHierro();
    }

    public void ModificarInputCarbono()
    {
        inputCarbono.text = Math.Round(sliderCarbono.value, 2) + "%";
    }

    public void ModificarSliderCarbono()
    {
        if (inputCarbono.text == "")
        {
            sliderCarbono.value = 0f;
            inputCarbono.text = "0%";
        }
        else
        {
            string valorIngresado = inputCarbono.text.Replace("%", "");
            sliderCarbono.value = float.Parse(valorIngresado);
        }
    }

    public void ModificarInputNiquel()
    {
        inputNiquel.text = Math.Round(sliderNiquel.value, 2) + "%";
    }

    public void ModificarSliderNiquel()
    {
        if (inputNiquel.text == "")
        {
            sliderNiquel.value = 0f;
            inputNiquel.text = "0%";
        }
        else
        {
            string valorIngresado = inputNiquel.text.Replace("%", "");
            sliderNiquel.value = float.Parse(valorIngresado);
        }
    }

    public void ModificarInputCromo()
    {
        inputCromo.text = Math.Round(sliderCromo.value, 2) + "%";
    }

    public void ModificarSliderCromo()
    {
        if (inputCromo.text == "")
        {
            sliderCromo.value = 0f;
            inputCromo.text = "0%";
        }
        else
        {
            string valorIngresado = inputCromo.text.Replace("%", "");
            sliderCromo.value = float.Parse(valorIngresado);
        }
    }

    public void ModificarInputMolibdeno()
    {
        inputMolibdeno.text = Math.Round(sliderMolibdeno.value, 2) + "%";
    }

    public void ModificarSliderMolibdeno()
    {
        if (inputMolibdeno.text == "")
        {
            sliderMolibdeno.value = 0f;
            inputMolibdeno.text = "0%";
        }
        else
        {
            string valorIngresado = inputMolibdeno.text.Replace("%", "");
            sliderMolibdeno.value = float.Parse(valorIngresado);
        }
    }

    public void ModificarInputVanadio()
    {
        inputVanadio.text = Math.Round(sliderVanadio.value, 2) + "%";
    }

    public void ModificarSliderVanadio()
    {
        if (inputVanadio.text == "")
        {
            sliderVanadio.value = 0f;
            inputVanadio.text = "0%";
        }
        else
        {
            string valorIngresado = inputVanadio.text.Replace("%", "");
            sliderVanadio.value = float.Parse(valorIngresado);
        }
    }

    public void ModificarInputHierro()
    {
        inputHierro.text = Math.Round(sliderHierro.value, 2) + "%";
    }
}