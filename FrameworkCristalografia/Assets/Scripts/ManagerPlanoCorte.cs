using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPlanoCorte : MonoBehaviour
{
    public Slider sliderX, sliderY, sliderZ;
    private Vector3 pointOnXAxis, pointOnYAxis, pointOnZAxis;

    void Update()
    {
        if (sliderX.value == 0)
        {
            pointOnXAxis.x = 9999;
        }
        else
        {
            pointOnXAxis.x = sliderX.value;
        }

        if (sliderY.value == 0)
        {
            pointOnYAxis.y = 9999;
        }
        else
        {
            pointOnYAxis.y = sliderY.value;
        }

        if (sliderZ.value == 0)
        {
            pointOnZAxis.z = 9999;
        }
        else
        {
            pointOnZAxis.z = sliderZ.value;
        }

        // Calculamos el vector normal del plano
        Vector3 normal = CalcularNormal(pointOnXAxis, pointOnYAxis, pointOnZAxis);

        // Ajustamos la rotación y posición del plano para que se alinee con la normal calculada
        RotarPlano(normal);

        // Posicionamos el plano para que quede en la celda unitaria
        PosicionarPlano();
    }

    // Función para calcular el vector normal de un plano dado por tres puntos
    private Vector3 CalcularNormal(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        // Obtener dos vectores en el plano
        Vector3 vector1 = point2 - point1;
        Vector3 vector2 = point3 - point1;

        // Calcular el producto cruzado para obtener el vector normal
        Vector3 normal = Vector3.Cross(vector1, vector2);

        // Normalizamos el vector normal
        return normal.normalized;
    }

    // Función para rotar el plano en función de la normal calculada
    private void RotarPlano(Vector3 normal)
    {
        // Calcular la rotación que alinea el vector forward del plano con la normal
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, normal);

        // Aplicar la rotación al objeto del plano
        transform.localRotation = targetRotation;
    }

    private void PosicionarPlano()
    {
        Plane p = new Plane();
        p.Set3Points(pointOnXAxis, pointOnYAxis, pointOnZAxis);

        Vector3 n = p.normal.normalized;

        float distance = p.distance;
        transform.localPosition = (n * -distance) + new Vector3(-0.5f, -0.5f, -0.5f);
    }
    
}
