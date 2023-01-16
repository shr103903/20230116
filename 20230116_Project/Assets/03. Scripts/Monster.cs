using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player = null;

    private float speed = 7f;

    void Start()
    {
        
    }

    void Update()
    {
        if(player != null)
        {
            transform.LookAt(player);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
