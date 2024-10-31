using UnityEngine;

public class DibujarPerimetro : MonoBehaviour
{
    public Material cutMaterial; // Asigna el material que usa el shader
    public Transform planeTransform; // Objeto que representa el plano de corte

    void Update()
    {
        // Obtén la posición y la normal del plano desde el Transform
        Vector3 planePos = planeTransform.position;
        Vector3 planeNormal = planeTransform.up; // Supón que el eje 'Y' es la normal del plano

        // Actualiza el shader con la nueva posición y orientación del plano
        cutMaterial.SetVector("_CutPlanePos", new Vector4(planePos.x, planePos.y, planePos.z, 0));
        cutMaterial.SetVector("_CutPlaneNormal", new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, 0));
    }
}
