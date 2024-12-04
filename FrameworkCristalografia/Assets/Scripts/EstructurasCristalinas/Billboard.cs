using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject camara;

    void Update()
    {
        transform.rotation = Quaternion.Euler(-camara.transform.rotation.eulerAngles.x + 90f, camara.transform.rotation.eulerAngles.y + 180f, 0f);
    }
}