using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    public Camera captureCamera;       // Cámara desde la cual capturar
    public RenderTexture renderTexture; // RenderTexture con soporte para alfa
    public RawImage rawImageDisplay;   // RawImage para mostrar la captura
    private Texture2D currentTexture;  // Referencia a la textura actual para liberar memoria

    public void CapturarCamara()
    {
        Invoke("CaptureAndApplyToRawImage", 0.1f);
    }

    private void CaptureAndApplyToRawImage()
    {
        // Configura el RenderTexture de la cámara
        captureCamera.targetTexture = renderTexture;

        // Configura el RenderTexture como activo
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        // Crear una nueva Texture2D con soporte para alfa
        Texture2D newTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        newTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        newTexture.Apply();

        // Restaurar el RenderTexture activo
        RenderTexture.active = currentRT;

        // Reemplazar la textura actual en el RawImage
        if (currentTexture != null)
        {
            Destroy(currentTexture); // Liberar memoria de la textura previa
        }

        currentTexture = newTexture;
        rawImageDisplay.texture = currentTexture;
    }

}