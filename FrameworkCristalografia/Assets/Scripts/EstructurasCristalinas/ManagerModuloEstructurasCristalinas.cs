using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerModuloEstructurasCristalinas : MonoBehaviour
{
    public Button generarCorte;

    void Start()
    {
        generarCorte.onClick.Invoke();
    }

    public void Atras()
    {
        SceneManager.LoadScene("InicioApp");
    }
}