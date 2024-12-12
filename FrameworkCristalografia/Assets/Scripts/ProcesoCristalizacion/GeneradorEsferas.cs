using UnityEngine;
using UnityEngine.UI;

public class GeneradorEsferas : MonoBehaviour
{
    public GameObject spherePrefab;
    public float minX, maxX, minY, maxY;
    public Transform contenedorBolas;
    public Slider sliderProporcion;

    private float filaActual, columnaActual, separacion, proporcion;

    void Start()
    {
        separacion = 0.141f;
    }

    public void GenerarSimulacion()
    {
        filaActual = minY;
        columnaActual = minX;
        proporcion = sliderProporcion.value;

        VaciarContenedor();
        InvokeRepeating("CrearFila", 0f, 0.015f);
    }
    
    private void CrearFila()
    {
        if (filaActual <= maxY)
        {
            while (columnaActual <= maxX)
            {
                Vector3 newPosition = new Vector3(columnaActual, filaActual, 0);
                GameObject nuevaBolita = Instantiate(spherePrefab, newPosition, transform.rotation);
                nuevaBolita.transform.SetParent(contenedorBolas);

                if (Random.Range(1, 100) <= proporcion){
                    nuevaBolita.transform.localScale *= 0.53f;
                    nuevaBolita.GetComponent<Renderer>().material.color = Color.red;
                }
                
                columnaActual += separacion;
            }

            columnaActual = minX;
            filaActual += separacion;
        }
    }

    private void VaciarContenedor()
    {
        CancelInvoke("CrearFila");
        
        foreach (Transform bola in contenedorBolas)
        {
            Destroy(bola.gameObject);
        }
    }
}