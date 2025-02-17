using System;
using UnityEngine;
using UnityEngine.UI;

public class LogicaDiagramas : MonoBehaviour
{
    public Camera camara;
    public ManagerInterfaceDF managerInterface;
    private bool modoCuNi, modoPbSn, modoFeC;

    void Start()
    {
        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;
    }

    void Update()
    {
        if (Input.touchCount == 1) 
        {
            Touch touch = Input.GetTouch(0); 

            if (touch.phase == TouchPhase.Began) 
            {
                Ray ray = camara.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) // Si no toca dentro del grafico, no entra a este if porque el rayo sigue al infinito
                {
                    if (modoCuNi)
                    { 
                        ManagerCobreNiquel(hit.point);
                    }
                    else if (modoPbSn)
                    {
                        //ManagerPlomoEstano(hit)
                    }
                    else if (modoFeC)
                    {
                        //ManagerHierroCarbono(hit)
                    }
                }
            }
        }
    }

    public void EstablecerNuevoPunto()
    {
        Invoke("EstablecerNuevoPuntoWrap", 0.01f);
    }
    
    private void EstablecerNuevoPuntoWrap()
    {
        float posicionY = (managerInterface.ObtenerTemperatura() - 1000) / 5;
        float posicionX = managerInterface.ObtenerPorcentajeFaseUno();

        Vector3 posicionNuevoPunto = new Vector3(posicionX, 0f, posicionY);

        ManagerCobreNiquel(posicionNuevoPunto);
    }

    private void ManagerCobreNiquel(Vector3 origen)
    {
        float temperatura = (float) Math.Round((origen.z / 2 * 10) + 1000, 1);
        float porcentajeCobre = (float) Math.Round(origen.x, 2);
        float porcentajeNiquel = (float) Math.Round(100 - porcentajeCobre, 2);

        RaycastHit hit;

        if (Physics.Raycast(origen, Vector3.back, out hit))
        {
            float porcentajeFaseAlpha = 0;
            float porcentajeFaseLiquida = 0;

            float L = 0;
            float C = origen.x;
            float S = 0;
            
            if (hit.collider.CompareTag("LiquidusLine")) // Estoy en zona liquida
            {
                porcentajeFaseLiquida = 100f;
                porcentajeFaseAlpha = 0f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida");
            }
            else if (hit.collider.CompareTag("SolidusLine")) // Estoy en zona liquida + alpha
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    S = hit.point.x;
                }

                porcentajeFaseAlpha = (C-L) / (S-L) * 100;
                porcentajeFaseLiquida = (S-C) / (S-L) * 100;
                
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida + Alpha");
            }
            else if (hit.collider.CompareTag("LimiteDelGrafico")) // Estoy en zona solida
            {
                porcentajeFaseLiquida = 0f;
                porcentajeFaseAlpha = 100f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("Sólida");
            }
            managerInterface.ActualizarTemperatura(temperatura);
            managerInterface.ActualizarPorcentajeFaseUno(porcentajeCobre);
            managerInterface.ActualizarPorcentajeFaseDos(porcentajeNiquel);

            managerInterface.ActualizarResultados(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeNiquel, porcentajeCobre, L, C, S);
        }
    }
}