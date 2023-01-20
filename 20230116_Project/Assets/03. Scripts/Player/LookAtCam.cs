using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private Transform cam = null;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.back, cam.rotation * Vector3.down);
    }
}
