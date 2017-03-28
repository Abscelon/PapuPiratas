using UnityEngine;
using System.Collections;

// mouse center button = int 2
public class cssCameraControl : MonoBehaviour
{
    private void Update()
    {
        // movement
        if(Input.GetMouseButton(2))
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition),
                0.01f);
        }
        if(Input.mouseScrollDelta.magnitude > 0)
        {
            Camera.main.orthographicSize -= (Input.mouseScrollDelta.y * 0.5f);
        }
    }
}
