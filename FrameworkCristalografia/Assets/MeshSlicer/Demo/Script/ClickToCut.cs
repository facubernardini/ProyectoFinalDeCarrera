using com.marufhow.meshslicer.core;
using UnityEngine;

namespace com.marufhow.meshslicer.demo
{
    public class ClickToCut : MonoBehaviour
    {
        public MHCutter _mhCutter;
        public GameObject planoCorte;
        public GameObject[] celdas;
        private Vector3 posicionCorte, direccionCorte;

        private void Update()
        {
            posicionCorte = planoCorte.transform.position;
            direccionCorte = planoCorte.transform.up;
        }

        public void CortarMallas()
        {
            for (int i = 0; i < celdas.Length; i++)
            {
                if (celdas[i].activeSelf)
                {
                    _mhCutter.Cut(celdas[i], posicionCorte, direccionCorte);
                }
            }
        }
    }
}