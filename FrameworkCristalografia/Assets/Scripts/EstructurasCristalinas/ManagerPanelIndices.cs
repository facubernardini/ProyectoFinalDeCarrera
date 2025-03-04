using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPanelIndices : MonoBehaviour
{
    public Slider sliderX, sliderZ, sliderY;
    public TextMeshProUGUI valorX, valorY, valorZ;
    public Button botonNormalizarPlano;
    public Toggle toggleModoContinuo;
    public TMP_InputField inputIndiceX, inputIndiceY, inputIndiceZ;
    private bool modoContinuo, modoFraccion;
    private float umbral;

    void Start()
    {
        modoContinuo = false;
        modoFraccion = false;

        umbral = 0.1f;

        valorX.text = sliderX.value + "";
        valorY.text = sliderY.value + "";
        valorZ.text = sliderZ.value + "";

        botonNormalizarPlano.interactable = false;
    }
    
    public void ActivarModoContinuo()
    {
        if (!modoContinuo)
        {
            sliderX.wholeNumbers = false;
            sliderY.wholeNumbers = false;
            sliderZ.wholeNumbers = false;
            
            botonNormalizarPlano.interactable = true;
        }
        else
        {
            sliderX.wholeNumbers = true;
            sliderY.wholeNumbers = true;
            sliderZ.wholeNumbers = true;

            botonNormalizarPlano.interactable = false;
        }

        modoContinuo = !modoContinuo;
    }

    public void ActivarModoFraccion()
    {
        if (!modoFraccion)
        {
            sliderX.minValue = 1;
            sliderY.minValue = 1;
            sliderZ.minValue = 1;

            toggleModoContinuo.isOn = false;
            toggleModoContinuo.interactable = false;
        }
        else
        {
            sliderX.minValue = 0;
            sliderY.minValue = 0;
            sliderZ.minValue = 0;

            toggleModoContinuo.interactable = true;
        }

        modoFraccion = !modoFraccion;

        sliderX.value = 1;
        sliderY.value = 1;
        sliderZ.value = 1;

        ActualizarValorX();
        ActualizarValorY();
        ActualizarValorZ();
    }

    public void ActualizarValorX()
    {
        if (modoFraccion)
        {
            if (sliderX.value == 1)
            {
                valorX.text = 1 + "";
            }
            else
            {
                valorX.text = "1/" + Math.Round(sliderX.value, 2);
            }
        }
        else
        {
            valorX.text = Math.Round(sliderX.value, 2) + "";
        }
    }

    public void ActualizarValorY()
    {
        if (modoFraccion)
        {
            if (sliderY.value == 1)
            {
                valorY.text = 1 + "";
            }
            else
            {
                valorY.text = "1/" + Math.Round(sliderY.value, 2);
            }
        }
        else
        {
            valorY.text = Math.Round(sliderY.value, 2) + "";
        }
    }

    public void ActualizarValorZ()
    {
        if (modoFraccion)
        {
            if (sliderZ.value == 1)
            {
                valorZ.text = 1 + "";
            }
            else
            {
                valorZ.text = "1/" + Math.Round(sliderZ.value, 2);
            }
        }
        else
        {
            valorZ.text = Math.Round(sliderZ.value, 2) + "";
        }
    }

    public void ValorEnteroSlider()
    {
        if (modoContinuo)
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

    public void GenerarPlanoSegunIndicesDeMiller()
    {
        if (!modoFraccion)
        {
            int h = int.Parse(inputIndiceX.text);
            int k = int.Parse(inputIndiceY.text);
            int l = int.Parse(inputIndiceZ.text);

            if (h != 0 || k != 0 || l != 0)
            {
                // Encontrar el MCM de los valores no nulos
                int mcm = LCM_NonZero(h, k, l);

                // Calcular las intersecciones (cuidando la división por 0)
                int x = (h == 0) ? 0 : mcm / h;
                int y = (k == 0) ? 0 : mcm / k;
                int z = (l == 0) ? 0 : mcm / l;

                sliderX.value = x;
                sliderY.value = y;
                sliderZ.value = z;
            }
        }
    }

    public void ActualizarIndicesMiller()
    {
        if (!modoContinuo)
        {
            if (modoFraccion)
            {
                inputIndiceX.text = sliderX.value + "";
                inputIndiceY.text = sliderY.value + "";
                inputIndiceZ.text = sliderZ.value + "";
            }
            else
            {
                int valorX = (int) sliderX.value;
                int valorY = (int) sliderY.value;
                int valorZ = (int) sliderZ.value;

                // Calcular los recíprocos (contemplo casos donde los valores son cero)
                double hx = (valorX == 0) ? 0 : 1.0 / valorX;
                double ky = (valorY == 0) ? 0 : 1.0 / valorY;
                double lz = (valorZ == 0) ? 0 : 1.0 / valorZ;

                // Encuentra el minimo comun multiplo (entre los valores distintos de cero)
                int mcd = LCM_NonZero(valorX, valorY, valorZ); 

                // Multiplicar los recíprocos por el MCD
                int h = (valorX == 0) ? 0 : (int)(hx * mcd);
                int k = (valorY == 0) ? 0 : (int)(ky * mcd);
                int l = (valorZ == 0) ? 0 : (int)(lz * mcd);

                inputIndiceX.text = h + "";
                inputIndiceY.text = k + "";
                inputIndiceZ.text = l + "";
            }
        }
    }

    private int LCM_NonZero(int a, int b, int c)
    {
        int[] valores = new int[3];
        int count = 0;

        if (a > 0) valores[count++] = a;
        if (b > 0) valores[count++] = b;
        if (c > 0) valores[count++] = c;

        if (count == 0) return 1; // En caso extremo donde todos sean 0 (no suele pasar en cristalografía)

        // Calcular el LCM acumulado de los valores no cero
        int lcm = valores[0];
        for (int i = 1; i < count; i++)
        {
            lcm = LCM(lcm, valores[i]);
        }
        return lcm;
    }

    private int LCM(int a, int b)
    {
        return (a * b) / GCD(a, b);
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}