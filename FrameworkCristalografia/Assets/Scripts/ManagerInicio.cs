using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerInicio : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 144;
    }

    public void IniciarModuloEstructurasCristalinas()
    {
        SceneManager.LoadScene("ModuloEstructurasCristalinas");
    }

    public void IniciarModuloProcesoCristalizacion()
    {
        SceneManager.LoadScene("ModuloProcesoCristalizacion");
    }

    public void IniciarModuloDiagramaDeFases()
    {
        SceneManager.LoadScene("ModuloDiagramaDeFases");
    }

    public void IniciarModuloRealidadAumentada()
    {
        SceneManager.LoadScene("ModuloRealidadAumentada");
    }
}
