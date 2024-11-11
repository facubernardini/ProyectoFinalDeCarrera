using com.marufhow.meshslicer.core;
using UnityEngine;

namespace com.marufhow.meshslicer.demo
{
    public class ClickToCut : MonoBehaviour
    {
        public MHCutter _mhCutter;
        public GameObject planoCorte, cubo;
        private Vector3 posicionCorte, direccionCorte;

        private void Update()
        {
            posicionCorte = planoCorte.transform.position;
            direccionCorte = planoCorte.transform.up;

            if (Input.GetMouseButtonDown(0))
            {
                _mhCutter.Cut(cubo, posicionCorte, direccionCorte);
            }
        }
    }
}