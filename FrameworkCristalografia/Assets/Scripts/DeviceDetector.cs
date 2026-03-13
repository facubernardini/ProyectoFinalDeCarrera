using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class DeviceDetector : MonoBehaviour
{
    #if !UNITY_EDITOR && UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern bool IsMobileBrowser();
    #endif

    public GameObject myCustomKeyboard;

    public bool IsMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobileBrowser();
        #else
            return Application.isMobilePlatform;
        #endif
    }

    public void OnInputSelected(TMP_InputField input)
    {
        if (IsMobile())
        {
            var keyboardScript = myCustomKeyboard.GetComponent<NumericKeyboard>();
            if (keyboardScript != null)
            {
                keyboardScript.SetTargetInput(input);
            }
        }
    }
}