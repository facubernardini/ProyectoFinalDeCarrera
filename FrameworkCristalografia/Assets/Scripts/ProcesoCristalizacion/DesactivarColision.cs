using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarColision : MonoBehaviour
{
    void Start()
    {
        Invoke("StopCollider", 8);
    }

    void StopCollider()
    {
        Destroy(GetComponent<Rigidbody>());
    }
}
