using UnityEngine;

public class CorteConTapa : MonoBehaviour
{
    public Material cutMaterialCeleste;
    public Material cutMaterialNaranja;
    public Material cutMaterialVerde; 
    public Transform planoDeCorte; 

    void Update()
    {
        // Obtén la posición y la normal del plano desde el Transform
        Vector3 planePos = planoDeCorte.position;
        Vector3 planeNormal = planoDeCorte.up; 

        // Actualiza el shader con la nueva posición y orientación del plano
        cutMaterialCeleste.SetVector("_CutPlanePos", new Vector4(planePos.x, planePos.y, planePos.z, 0));
        cutMaterialCeleste.SetVector("_CutPlaneNormal", new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, 0));

        cutMaterialNaranja.SetVector("_CutPlanePos", new Vector4(planePos.x, planePos.y, planePos.z, 0));
        cutMaterialNaranja.SetVector("_CutPlaneNormal", new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, 0));

        cutMaterialVerde.SetVector("_CutPlanePos", new Vector4(planePos.x, planePos.y, planePos.z, 0));
        cutMaterialVerde.SetVector("_CutPlaneNormal", new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, 0));
    }
}
