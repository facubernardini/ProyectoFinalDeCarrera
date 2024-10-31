using UnityEngine;
using UnityEngine.UI;

public class ManagerOpciones : MonoBehaviour
{
    public GameObject panelInferior, panelOpciones, panelCeldaHexagonal;
    public GameObject CeldaCentradaCuerpo, CeldaCentradaCaras, CeldaHexagonalCompacta;
    public GameObject CeldaCentradaCuerpoExpandida, CeldaCentradaCarasExpandida, CeldaHexagonalExpandida;
    public Camera camaraPrincipal;
    public Toggle toggleExpandirEstructura;
    public Slider sliderTransparencia;
    public Material[] materialesTransparentes;
    private bool MenuOpcionesAbierto;

    void Start()
    {
        MenuOpcionesAbierto = false;
        panelInferior.SetActive(true);
        panelOpciones.SetActive(false);
        panelCeldaHexagonal.SetActive(false);

        CeldaCentradaCuerpo.SetActive(true);
        CeldaCentradaCaras.SetActive(false);
        CeldaHexagonalCompacta.SetActive(false);

        CeldaCentradaCuerpoExpandida.SetActive(true);
        CeldaCentradaCarasExpandida.SetActive(false);
        CeldaHexagonalExpandida.SetActive(false);
    }

    public void AbrirMenuOpciones()
    {
        if (!MenuOpcionesAbierto)
        {
            panelOpciones.SetActive(true);
            panelInferior.SetActive(false);
        }
        else
        {
            panelOpciones.SetActive(false);
            panelInferior.SetActive(true);
        }

        MenuOpcionesAbierto = !MenuOpcionesAbierto;
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

        foreach (Material mat in materialesTransparentes)
        {
            Color colorAux = mat.color;
            colorAux.a = valorTransparencia;
            mat.color = colorAux;
        }
    }

}