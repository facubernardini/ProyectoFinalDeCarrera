using UnityEngine;
using Vuforia;

public class ColocarObjetoAR : MonoBehaviour
{
    public PlaneFinderBehaviour planeFinder;

    public void ColocarObjeto()
    {
        Vector2 position = new Vector2(0, 0);
        planeFinder.GetComponent<PlaneFinderBehaviour>().PerformHitTest(position);

        planeFinder.gameObject.SetActive(false); // Oculta el marcador AR
    }

    public void ActivarMarcadorAR()
    {
        planeFinder.gameObject.SetActive(true);
    }
}
