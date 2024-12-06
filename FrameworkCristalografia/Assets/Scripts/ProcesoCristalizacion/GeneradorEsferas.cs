using UnityEngine;

public class GeneradorEsferas : MonoBehaviour
{
    public GameObject spherePrefab;
    public int minX, maxX, minY, maxY;

    private float filaActual, columnaActual, rango;

    void Start()
    {
        rango = 0.16f;
    }

    public void GenerarSimulacion()
    {
        filaActual = minY;
        columnaActual = minX;

        InvokeRepeating("CreateRow", 0f, 0.1f);
    }
    
    private void CreateRow()
    {
        if (filaActual <= maxY)
        {
            while (columnaActual <= maxX)
            {
                Vector3 newPosition = new Vector3(columnaActual, filaActual, 0);
                GameObject nuevaBolita = Instantiate(spherePrefab, newPosition, transform.rotation);

                if (Random.Range(0,5) == 0){
                    nuevaBolita.transform.localScale *= 0.53f;
                }

                columnaActual += rango;
            }

            columnaActual = minX;
            filaActual += rango;
        }
    }
}
