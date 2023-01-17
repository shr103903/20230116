using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;

    [SerializeField]
    private float height = 12f;

    [SerializeField]
    private float weight = 2f;

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, player.position + Vector3.up * height - Vector3.forward * weight, 0.1f);
    }
}
