using UnityEngine;

public class GeneradorEsferas : MonoBehaviour
{
    public GameObject spherePrefab;
    public float minX, maxX, minY, maxY;
    public Transform contenedorBolas;

    private float filaActual, columnaActual, separacion;

    void Start()
    {
        separacion = 0.141f;
    }

    public void GenerarSimulacion()
    {
        filaActual = minY;
        columnaActual = minX;

        VaciarContenedor();
        InvokeRepeating("CreateRow", 0f, 0.015f);
    }
    
    private void CreateRow()
    {
        if (filaActual <= maxY)
        {
            while (columnaActual <= maxX)
            {
                Vector3 newPosition = new Vector3(columnaActual, filaActual, 0);
                GameObject nuevaBolita = Instantiate(spherePrefab, newPosition, transform.rotation);
                nuevaBolita.transform.SetParent(contenedorBolas);

                if (Random.Range(0,5) == 0){
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
        CancelInvoke("CreateRow");
        
        foreach (Transform bola in contenedorBolas)
        {
            Destroy(bola.gameObject);
        }
    }
}
