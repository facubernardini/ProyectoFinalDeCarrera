using System;
using UnityEngine;

public class ManagerInteraccionGraficos : MonoBehaviour
{
    public Camera camara;
    public GameObject prefabPunto;
    private bool modoCuNi, modoPbSn, modoFeC;
    private GameObject punto;
    private ManagerInterfaceDF managerInterface;

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
                    Destroy(punto);
                    punto = Instantiate(prefabPunto, hit.point, transform.rotation);

                    if (modoCuNi)
                    { 
                        ManagerCobreNiquel(hit);
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

    private void ManagerCobreNiquel(RaycastHit hit)
    {
        float temperatura = (float) Math.Round((hit.point.z / 2 * 10) + 1000, 2);
        float porcentajeCobre = (float) Math.Round(hit.point.x, 2);
        float porcentajeNiquel = (float) Math.Round(100 - porcentajeCobre, 2);
        Debug.Log("Temperatura: " + temperatura + " - Cobre: " + porcentajeCobre + "% - Niquel: " + porcentajeNiquel + "%");

        Vector3 origen = new Vector3(hit.point.x, 0f, hit.point.z);

        if (Physics.Raycast(origen, Vector3.back, out hit))
        {
            if (hit.collider.CompareTag("LiquidusLine")) // Estoy en zona liquida
            {
                // Actualizar interfaz mostrando que la aleacion se encuentra en estado 100% LIQUIDO
                // y la composicion de ese liquido es X% cobre e Y% niquel
            }
            else if (hit.collider.CompareTag("SolidusLine")) // Estoy en zona liquida + alpha
            {
                float S = 0;
                float L = 0;
                float C = origen.x;

                if (Physics.Raycast(origen, Vector3.right, out hit))
                {
                    S = hit.point.x;
                }

                if (Physics.Raycast(origen, Vector3.left, out hit))
                {
                    L = hit.point.x;
                }

                float porcentajeFaseAlpha = ((C-L) / (S-L)) * 100;
                float porcentajeFaseLiquida = ((S-C) / (S-L)) * 100;

                porcentajeFaseAlpha = (float) Math.Round(porcentajeFaseAlpha, 2);
                porcentajeFaseLiquida = (float) Math.Round(porcentajeFaseLiquida, 2);

                Debug.Log("Porcentaje fase alpha: " + porcentajeFaseAlpha);
                Debug.Log("Porcentaje fase liquida: " + porcentajeFaseLiquida);
            }
            else if (hit.collider.CompareTag("LimiteDelGrafico")) // Estoy en zona solida
            {
                // Actualizar interfaz mostrando que la aleacion se encuentra en estado 100% SOLIDO
                // y la composicion de ese solido es X% cobre e Y% niquel
            }
        }
    }
}