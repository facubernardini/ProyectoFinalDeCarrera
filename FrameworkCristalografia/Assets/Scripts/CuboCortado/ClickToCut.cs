using com.marufhow.meshslicer.core;
using UnityEngine;

namespace com.marufhow.meshslicer.demo
{
    public class ClickToCut : MonoBehaviour
    {
        public MHCutter _mhCutter;
        public GameObject planoCorte;

        public GameObject centradaCuerpoCentro, centradaCuerpoEsquinas;
        public GameObject centradaCarasA, centradaCarasB, centradaCarasC;
        public GameObject hexagonalCompactaA, hexagonalCompactaB;
        public GameObject centradaCuerpo, centradaCaras, hexagonalCompacta;
        public GameObject iconoLoading, vistaCorte;

        private Vector3 posicionCorte, direccionCorte;
        private GameObject corteActualA, corteActualB, corteActualC;

        private void Update()
        {
            posicionCorte = planoCorte.transform.position;
            direccionCorte = planoCorte.transform.up;
        }

        public void CortarMallasWrapper()
        {
            Invoke("CortarMallas", 0.05f);
        }

        private void CortarMallas()
        {
            if (centradaCuerpo.activeSelf)
            {
                Destroy(corteActualA);
                Destroy(corteActualB);

                corteActualA = _mhCutter.Cut(Instantiate(centradaCuerpoCentro), posicionCorte, direccionCorte);
                corteActualB = _mhCutter.Cut(Instantiate(centradaCuerpoEsquinas), posicionCorte, direccionCorte);
            }

            if (centradaCaras.activeSelf)
            {
                Destroy(corteActualA);
                Destroy(corteActualB);
                Destroy(corteActualC);

                corteActualA = _mhCutter.Cut(Instantiate(centradaCarasA), posicionCorte, direccionCorte);
                corteActualB = _mhCutter.Cut(Instantiate(centradaCarasB), posicionCorte, direccionCorte);
                corteActualC = _mhCutter.Cut(Instantiate(centradaCarasC), posicionCorte, direccionCorte);
            }

            if (hexagonalCompacta.activeSelf)
            {
                Destroy(corteActualA);
                Destroy(corteActualB);

                corteActualA = _mhCutter.Cut(Instantiate(hexagonalCompactaA), posicionCorte, direccionCorte);
                corteActualB = _mhCutter.Cut(Instantiate(hexagonalCompactaB), posicionCorte, direccionCorte);
            }

            iconoLoading.SetActive(false);
            vistaCorte.SetActive(true);
        }

        public void ActualizarVistaCorte()
        {
            Destroy(corteActualA);
            Destroy(corteActualB);
            Destroy(corteActualC);
        }

    }
}