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

                corteActualA.transform.SetParent(centradaCuerpo.transform, false);
                corteActualB.transform.SetParent(centradaCuerpo.transform, false);
            }

            if (centradaCaras.activeSelf)
            {
                Destroy(corteActualA);
                Destroy(corteActualB);
                Destroy(corteActualC);

                corteActualA = _mhCutter.Cut(Instantiate(centradaCarasA), posicionCorte, direccionCorte);
                corteActualB = _mhCutter.Cut(Instantiate(centradaCarasB), posicionCorte, direccionCorte);
                corteActualC = _mhCutter.Cut(Instantiate(centradaCarasC), posicionCorte, direccionCorte);

                corteActualA.transform.SetParent(centradaCaras.transform, false);
                corteActualB.transform.SetParent(centradaCaras.transform, false);
                corteActualC.transform.SetParent(centradaCaras.transform, false);
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