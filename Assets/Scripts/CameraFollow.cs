using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform plane;
    public Vector3 cameraOffset;

    void Update()
    {
        if(plane != null)
            transform.position = plane.position + cameraOffset;
    }
}
