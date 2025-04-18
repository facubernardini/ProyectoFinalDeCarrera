using UnityEngine;
using UnityEngine.UI;

public class ManagerOpciones : MonoBehaviour
{
    public GameObject panelOpciones, panelCeldaHexagonal;
    public GameObject CeldaCentradaCuerpo, CeldaCentradaCaras, CeldaHexagonalCompacta;
    public GameObject CeldaCentradaCuerpoExpandida, CeldaCentradaCarasExpandida, CeldaHexagonalExpandida;
    public Camera camaraPrincipal;
    public Toggle mostrarPlanoDeCorte;
    public Slider sliderTransparencia;
    public Material[] materialesTransparentesVistaExpandida;
    public Material[] materialesTransparentesCeldas;
    public Button botonGenerarCorte, botonOpciones;
    public GameObject vistaCorte, planosCorteCeldaHexagonal;
    public Sprite imagenOpciones, imagenSalir;

    private bool menuOpcionesAbierto, sistemaDeslizamientoActivo;

    void Start()
    {
        menuOpcionesAbierto = false;
        sistemaDeslizamientoActivo = false;

        panelOpciones.SetActive(false);
        panelCeldaHexagonal.SetActive(false);

        CeldaCentradaCuerpo.SetActive(true);
        CeldaCentradaCaras.SetActive(false);
        CeldaHexagonalCompacta.SetActive(false);

        CeldaCentradaCuerpoExpandida.SetActive(true);
        CeldaCentradaCarasExpandida.SetActive(false);
        CeldaHexagonalExpandida.SetActive(false);

        botonGenerarCorte.interactable = true;
        vistaCorte.SetActive(false);
        planosCorteCeldaHexagonal.SetActive(false);
    }

    public void AbrirMenuOpciones()
    {
        if (!menuOpcionesAbierto)
        {
            panelOpciones.SetActive(true);
        }
        else
        {
            panelOpciones.SetActive(false);
        }

        menuOpcionesAbierto = !menuOpcionesAbierto;
    }

    public void SeleccionarCeldaCentradaCuerpo()
    {
        if (!CeldaCentradaCuerpo.activeSelf) 
        {
            CeldaCentradaCuerpo.SetActive(true);
            CeldaCentradaCaras.SetActive(false);
            CeldaHexagonalCompacta.SetActive(false);

            CeldaCentradaCuerpoExpandida.SetActive(true);
            CeldaCentradaCarasExpandida.SetActive(false);
            CeldaHexagonalExpandida.SetActive(false);

            panelCeldaHexagonal.SetActive(false);

            botonGenerarCorte.interactable = true;
            vistaCorte.SetActive(false);
            planosCorteCeldaHexagonal.SetActive(false);

            mostrarPlanoDeCorte.interactable = true;
        }
    }

    public void SeleccionarCeldaCentradaCaras()
    {
        if (!CeldaCentradaCaras.activeSelf) 
        {
            CeldaCentradaCuerpo.SetActive(false);
            CeldaCentradaCaras.SetActive(true);
            CeldaHexagonalCompacta.SetActive(false);
            
            CeldaCentradaCuerpoExpandida.SetActive(false);
            CeldaCentradaCarasExpandida.SetActive(true);
            CeldaHexagonalExpandida.SetActive(false);

            panelCeldaHexagonal.SetActive(false);

            botonGenerarCorte.interactable = true;
            vistaCorte.SetActive(false);
            planosCorteCeldaHexagonal.SetActive(false);

            mostrarPlanoDeCorte.interactable = true;
        }
    }

    public void SeleccionarCeldaHexagonal()
    {
        if (!CeldaHexagonalCompacta.activeSelf) 
        {
            CeldaCentradaCuerpo.SetActive(false);
            CeldaCentradaCaras.SetActive(false);
            CeldaHexagonalCompacta.SetActive(true);

            CeldaCentradaCuerpoExpandida.SetActive(false);
            CeldaCentradaCarasExpandida.SetActive(false);
            CeldaHexagonalExpandida.SetActive(true);

            panelCeldaHexagonal.SetActive(true);

            mostrarPlanoDeCorte.isOn = false;
            mostrarPlanoDeCorte.interactable = false;

            botonGenerarCorte.interactable = false;
            vistaCorte.SetActive(false);
            planosCorteCeldaHexagonal.SetActive(true);
        }
    }

    public void MostrarPlanoCorte()
    {
        camaraPrincipal.cullingMask ^= 1 << LayerMask.NameToLayer("PlanoCorte");
    }

    public void MostrarEjes()
    {
        camaraPrincipal.cullingMask ^= 1 << LayerMask.NameToLayer("Ejes");
    }

    public void MostrarVistaExpandida()
    {
        camaraPrincipal.cullingMask ^= 1 << LayerMask.NameToLayer("BolasTransparentes");
        sliderTransparencia.interactable = !sliderTransparencia.IsInteractable();
    }

    public void CambiarTransparencia()
    {
        float valorTransparencia = sliderTransparencia.value;

        foreach (Material mat in materialesTransparentesVistaExpandida)
        {
            Color colorAux = mat.color;
            colorAux.a = valorTransparencia;
            mat.color = colorAux;
        }
    }

    public void SistemaDeslizamiento()
    {
        camaraPrincipal.cullingMask ^= 1 << LayerMask.NameToLayer("SistemaDeDeslizamiento");

        if (!sistemaDeslizamientoActivo)
        {
            int i = 1; // Para que no queden todos los materiales con el mismo valor renderQueue y se glitcheen

            foreach (Material mat in materialesTransparentesCeldas)
            {
                mat.SetFloat("_Mode", 3);

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + i;
                i++;
            }
            sistemaDeslizamientoActivo = true;
        }
        else
        {
            foreach (Material mat in materialesTransparentesCeldas)
            {
                mat.SetFloat("_Mode", 0);

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 2000;
            }
            sistemaDeslizamientoActivo = false;
        }
        
    }

    public void CambiarIconoBotonOpciones()
    {
        if (menuOpcionesAbierto)
        {
            botonOpciones.image.sprite = imagenSalir;
        }
        else
        {
            botonOpciones.image.sprite = imagenOpciones;
        }
    }
}