using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerModuloCuboCortado : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 144;
    }

    public void Atras()
    {
        SceneManager.LoadScene("InicioApp");
    }
}