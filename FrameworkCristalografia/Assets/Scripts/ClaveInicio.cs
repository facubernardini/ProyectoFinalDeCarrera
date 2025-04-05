using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClaveInicio : MonoBehaviour
{
    public TMP_InputField inputClave;
    public GameObject textoClaveIncorrecta;
    private string clave;
    
    void Start()
    {
        clave = "materiales_UNS";
        textoClaveIncorrecta.SetActive(false);
        inputClave.text = "";
    }

    public void Ingresar()
    {
        if (inputClave.text == clave)
        {
            SceneManager.LoadScene("InicioApp");
        }
        else
        {
            inputClave.text = "";
            textoClaveIncorrecta.SetActive(true);
            Invoke("OcultarTextoClaveIncorrecta", 2);
        }
    }

    private void OcultarTextoClaveIncorrecta()
    {
        textoClaveIncorrecta.SetActive(false);
    }
}
