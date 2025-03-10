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
                            ManagerHierroCarbono(hit.point);
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
        if (modoFeC)
        {
            float posicionY = (managerInterface.ObtenerTemperatura() - 400) / 10;
            posicionX = posicionX * 100 / 6.7f;
            
            if ((posicionY >= 0 && posicionY <= 100) && (posicionX >= 0 && posicionX <= 100))
            {
                Vector3 posicionNuevoPunto = new Vector3(posicionX, 0f, posicionY);
                ManagerHierroCarbono(posicionNuevoPunto);
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
                porcentajeFaseLiquida = 100f;
                
                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida");
            }
            else if (hit.collider.CompareTag("AlphaLine")) // Zona Alpha
            {
                porcentajeFaseAlpha = 100f;

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
                managerInterface.ActualizarZona("α + Líquida");
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
                porcentajeFaseBeta = 100;

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
                
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("α + β");
            }

            managerInterface.ActualizarTemperatura(temperatura);
            managerInterface.ActualizarPorcentajeFaseUno(porcentajeEstano);
            managerInterface.ActualizarPorcentajeFaseDos(porcentajePlomo);

            managerInterface.ActualizarResultadosPlomoEstano(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeFaseBeta, porcentajeEstano, porcentajePlomo, L, C, S);
        }
    }

    private void ManagerHierroCarbono(Vector3 origen)
    {
        float temperatura = (float) Math.Round((origen.z * 10) + 400, 1);
        float porcentajeCarbono = (float) Math.Round(origen.x * 6.7f / 100, 2);
        float porcentajeHierro = (float) Math.Round(100 - porcentajeCarbono, 2);

        RaycastHit hit;

        if (Physics.Raycast(origen, Vector3.back, out hit))
        {
            float porcentajeFaseAlpha = 0;
            float porcentajeFaseGamma = 0;
            float porcentajeFaseFe3C = 0;
            float porcentajeFaseLiquida = 0;

            float L = 0;
            float C = origen.x;
            float S = 0;

            if (hit.collider.CompareTag("LiquidusLine")) // Zona Liquida
            {
                porcentajeFaseLiquida = 100;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida");
            }
            else if (hit.collider.CompareTag("L+Gamma")) // Zona L+Gamma
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
                porcentajeFaseGamma = (S-C) / (S-L) * 100;
            
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("ɣ + Líquida");
            }
            else if (hit.collider.CompareTag("L+Fe3C")) // Zona L+Fe3C
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                S = 100f;

                porcentajeFaseLiquida = (S-C) / (S-L) * 100;
                porcentajeFaseFe3C = (C-L) / (S-L) * 100;

                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("Líquida + Fe3C");
            }
            else if (hit.collider.CompareTag("Gamma")) // Zona Gamma
            {
                porcentajeFaseGamma = 100;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("ɣ");
            }
            else if (hit.collider.CompareTag("AlphaLine")) // Zona Alpha
            {
                porcentajeFaseAlpha = 100f;

                managerInterface.ColocarPuntoC(new Vector3(origen.x, 0f, origen.z));
                managerInterface.ActualizarZona("α");
            }
            else if (hit.collider.CompareTag("Alpha+Gamma")) // Zona Alpha+Gamma
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    S = hit.point.x;
                }

                porcentajeFaseGamma = (C-L) / (S-L) * 100;
                porcentajeFaseAlpha = (S-C) / (S-L) * 100;
            
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("α + ɣ");
            }
            else if (hit.collider.CompareTag("Gamma+Fe3C")) // Zona Gamma+Fe3C
            {
                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                S = 100f;

                porcentajeFaseGamma = (S-C) / (S-L) * 100;
                porcentajeFaseFe3C = (C-L) / (S-L) * 100;
                
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));

                managerInterface.ActualizarZona("ɣ + Fe3C");
            }
            else if (hit.collider.CompareTag("LimiteDelGrafico")) // Zona Alpha+Fe3C
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

                S = 100f;

                porcentajeFaseAlpha = (S-C) / (S-L) * 100;
                porcentajeFaseFe3C = (C-L) / (S-L) * 100;
                
                managerInterface.ColocarPuntos(new Vector3(L, 0f, origen.z), new Vector3(origen.x, 0f, origen.z), new Vector3(S, 0f, origen.z));
                managerInterface.ActualizarZona("α + Fe3C");
            }
        
            L = (float) Math.Round(L * 6.7f / 100, 2);
            C = (float) Math.Round(C * 6.7f / 100, 2);
            S = (float) Math.Round(S * 6.7f / 100, 2);

            managerInterface.ActualizarTemperatura(temperatura);
            managerInterface.ActualizarPorcentajeFaseUno(porcentajeCarbono);
            managerInterface.ActualizarPorcentajeFaseDos(porcentajeHierro);

            managerInterface.ActualizarResultadosHierroCarbono(porcentajeFaseLiquida, porcentajeFaseAlpha, porcentajeFaseGamma, porcentajeFaseFe3C, porcentajeCarbono, porcentajeHierro, L, C, S);
        }
    }

}