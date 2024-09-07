using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerModuloCuboCortado : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 144;
    }
}