using System;
using UnityEngine;

public class LogicaDiagramas : MonoBehaviour
{
    public Camera camara;
    public ManagerInterfaceDF managerInterface;
    private bool modoCuNi, modoPbSn, modoFeC, interaccionGrafico, puntoColocado;

    void Start()
    {
        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;

        interaccionGrafico = false;
        puntoColocado = false;
    }

    void Update()
    {
        if (interaccionGrafico)
        {
            if (Input.touchCount == 1 && !puntoColocado) 
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
                            ManagerPlomoEstano(hit.point);
                        }
                        else if (modoFeC)
                        {
                            //ManagerHierroCarbono(hit.point)
                        }

                        puntoColocado = true;
                    }
                    
                }
            }
        }
    }

    public void PuntoActualEliminado()
    {
        puntoColocado = false;
    }

    public void ActivarInteraccionGrafico()
    {
        interaccionGrafico = true;
    }

    public void DesactivarInteraccionGrafico()
    {
        interaccionGrafico = false;
    }

    public void ActivarModoCobreNiquel()
    {
        modoCuNi = true;
        modoPbSn = false;
        modoFeC = false;
        managerInterface.ModoCobreNiquel();
    }

    public void ActivarModoPlomoEstano()
    {
        modoCuNi = false;
        modoPbSn = true;
        modoFeC = false;
        managerInterface.ModoPlomoEstano();
    }

    public void ActivarModoHierroCarbono()
    {
        modoCuNi = false;
        modoPbSn = false;
        modoFeC = true;
        managerInterface.ModoHierroCarbono();
    }

    public void EstablecerNuevoPunto()
    {
        puntoColocado = true;
        Invoke("EstablecerNuevoPuntoWrap", 0.01f);
    }
    
    private void EstablecerNuevoPuntoWrap()
    {
        float posicionX = managerInterface.ObtenerPorcentajeFaseUno();

        if (modoCuNi)
        {
            float posicionY = (managerInterface.ObtenerTemperatura() - 1000) / 5;

            if (posicionY >= 0 && posicionY <= 100)
            {
                Vector3 posicionNuevoPunto = new Vector3(posicionX, 0f, posicionY);
                ManagerCobreNiquel(posicionNuevoPunto);
            }
        }
        if (modoPbSn)
        {
            float posicionY = managerInterface.ObtenerTemperatura() / 4;
            
            if (posicionY >= 0 && posicionY <= 100)
            {
                Vector3 posicionNuevoPunto = new Vector3(posicionX, 0f, posicionY);
                ManagerPlomoEstano(posicionNuevoPunto);
            }
        }
        
    }

    private void ManagerCobreNiquel(Vector3 origen)
    {
        float temperatura = (float) Math.Round((origen.z / 2 * 10) + 1000, 1);
        float porcentajeCobre = (float) Math.Round(100 - origen.x, 2);
        float porcentajeNiquel = (float) Math.Round(origen.x, 2);

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
                managerInterface.ActualizarZona("Líquida + α");
            }
            else if (hit.collider.CompareTag("LimiteDelGrafico")) // Estoy en zona alpha
            {
                porcentajeFaseLiquida = 0f;
                porcentajeFaseAlpha = 100f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("α");
            }
            managerInterface.ActualizarTemperatura(temperatura);
            managerInterface.ActualizarPorcentajeFaseUno(porcentajeNiquel);
            managerInterface.ActualizarPorcentajeFaseDos(porcentajeCobre);

            managerInterface.ActualizarResultadosCobreNiquel(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeNiquel, porcentajeCobre, L, C, S);
        }
    }

    private void ManagerPlomoEstano(Vector3 origen)
    {
        float temperatura = (float) Math.Round(origen.z * 4, 1);
        float porcentajeEstano = (float) Math.Round(origen.x, 2);
        float porcentajePlomo = (float) Math.Round(100 - porcentajeEstano, 2);

        RaycastHit hit;

        if (Physics.Raycast(origen, Vector3.back, out hit))
        {
            float porcentajeFaseAlpha = 0;
            float porcentajeFaseBeta = 0;
            float porcentajeFaseLiquida = 0;

            float L = 0;
            float C = origen.x;
            float S = 0;

            if (hit.collider.CompareTag("LiquidusLine")) // Zona Liquida
            {
                porcentajeFaseAlpha = 0;
                porcentajeFaseBeta = 0;
                porcentajeFaseLiquida = 100f;
                
                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida");
            }
            else if (hit.collider.CompareTag("AlphaLine")) // Zona Alpha
            {
                porcentajeFaseAlpha = 100f;
                porcentajeFaseBeta = 0;
                porcentajeFaseLiquida = 0f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("α");
            }
            else if (hit.collider.CompareTag("L+AlphaLine")) // Zona Liquida+Alpha
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    S = hit.point.x;
                }
                porcentajeFaseLiquida = (C-L) / (S-L) * 100;
                porcentajeFaseAlpha = (S-C) / (S-L) * 100;
            
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida + α");
            }
            else if (hit.collider.CompareTag("L+BetaLine")) // Zona Liquida+Beta
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    S = hit.point.x;
                }
                porcentajeFaseLiquida = (S-C) / (S-L) * 100;
                porcentajeFaseBeta = (C-L) / (S-L) * 100;
            
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida + β");
            }
            else if (hit.collider.CompareTag("BetaLine")) // Zona Beta
            {
                porcentajeFaseAlpha = 0f;
                porcentajeFaseBeta = 100;
                porcentajeFaseLiquida = 0f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("β");
            }
            else if (hit.collider.CompareTag("LimiteDelGrafico")) // Zona Alpha+Beta
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    if (hit.collider.CompareTag("LimiteDelGrafico"))
                    {
                        L = 0;
                    }
                    else
                    {
                        L = hit.point.x;
                    }
                }

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    if (hit.collider.CompareTag("LimiteDelGrafico"))
                    {
                        S = 100;
                    }
                    else
                    {
                        S = hit.point.x;
                    }
                }
                porcentajeFaseAlpha = (S-C) / (S-L) * 100;
                porcentajeFaseBeta = (C-L) / (S-L) * 100;
                porcentajeFaseLiquida = 0;
                
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("α + β");
            }

            managerInterface.ActualizarTemperatura(temperatura);
            managerInterface.ActualizarPorcentajeFaseUno(porcentajeEstano);
            managerInterface.ActualizarPorcentajeFaseDos(porcentajePlomo);

            managerInterface.ActualizarResultadosPlomoEstano(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeFaseBeta, porcentajeEstano, porcentajePlomo, L, C, S);
        }
    }

}