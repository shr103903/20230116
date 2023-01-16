using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private Transform playerTransform = null;

    private Vector3 moveVec = Vector3.zero;

    private float horizontal = 0;

    private float vertical = 0;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveVec.Set(horizontal, 0, vertical);

        if(moveVec.magnitude != 0)
        {
            playerTransform.position = playerTransform.position + moveVec * speed * Time.deltaTime;
        }
    }
}
