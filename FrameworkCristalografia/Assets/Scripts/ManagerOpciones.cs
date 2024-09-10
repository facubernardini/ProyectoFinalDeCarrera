using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerOpciones : MonoBehaviour
{
    public GameObject panelInferior, panelOpciones;
    public GameObject CeldaCentradaCuerpo, CeldaCentradaCaras, CeldaHexagonalCompacta;
    public GameObject CeldaCentradaCuerpoExpandida, CeldaCentradaCarasExpandida;
    public Camera camaraPrincipal;
    private bool MenuOpcionesAbierto;

    void Start()
    {
        MenuOpcionesAbierto = false;
        panelInferior.SetActive(true);
        panelOpciones.SetActive(false);

        CeldaCentradaCuerpo.SetActive(true);
        CeldaCentradaCaras.SetActive(false);
        CeldaHexagonalCompacta.SetActive(false);

        CeldaCentradaCuerpoExpandida.SetActive(false);
        CeldaCentradaCarasExpandida.SetActive(false);
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
        CeldaCentradaCuerpo.SetActive(true);
        CeldaCentradaCaras.SetActive(false);
        CeldaHexagonalCompacta.SetActive(false);

        CeldaCentradaCarasExpandida.SetActive(false);
    }

    public void SeleccionarCeldaCentradaCaras()
    {
        CeldaCentradaCuerpo.SetActive(false);
        CeldaCentradaCaras.SetActive(true);
        CeldaHexagonalCompacta.SetActive(false);
        
        CeldaCentradaCuerpoExpandida.SetActive(false);
    }

    public void SeleccionarCeldaHexagonal()
    {
        CeldaCentradaCuerpo.SetActive(false);
        CeldaCentradaCaras.SetActive(false);
        CeldaHexagonalCompacta.SetActive(true);
    }

    public void ExpandirEstructura()
    {
        if (CeldaCentradaCaras.activeSelf)
        {
            if (!CeldaCentradaCarasExpandida.activeSelf)
            {
                CeldaCentradaCarasExpandida.SetActive(true);
            }
            else
            {
                CeldaCentradaCarasExpandida.SetActive(false);
            }
                
        }

        if (CeldaCentradaCuerpo.activeSelf)
        {
            if (!CeldaCentradaCuerpoExpandida.activeSelf)
            {
                CeldaCentradaCuerpoExpandida.SetActive(true);
            }
            else
            {
                CeldaCentradaCuerpoExpandida.SetActive(false);
            }
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

}