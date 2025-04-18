using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerInicio : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
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

    public void Atras()
    {
        SceneManager.LoadScene("InicioApp");
    }

    public void AbrirPaginaUNS()
    {
        Application.OpenURL("https://www.uns.edu.ar/");
    }

    public void AbrirPaginaDCIC()
    {
        Application.OpenURL("https://cs.uns.edu.ar/");
    }

}