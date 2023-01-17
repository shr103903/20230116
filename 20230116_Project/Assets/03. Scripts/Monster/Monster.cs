using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player = null;

    private Animator anim = null;

    [SerializeField]
    private float distance = 0;

    private Rigidbody rigid = null;

    private Vector3 stopVec = Vector3.zero;

    private float speed = 7f;

    private bool isHit = false;

    private IEnumerator hitCor = null;

    private WaitForSeconds hitDelay = new WaitForSeconds(3.5f);

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        rigid.velocity = Vector3.zero;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (isHit)
        {
            transform.position = stopVec;
        }
    }

    private void Move()
    {
        if (player != null)
        {
            if (isHit)
            {
                return;
            }

            if (transform.position.y <= -1 || transform.position.y >= 1)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.05f);
            distance = Vector3.Distance(transform.position, player.position);

            if (distance < 3)
            {
                //transform.position = Vector3.Lerp(transform.position, player.position, 0.0005f);
            }
            else if (distance >= 3 && distance < 15)
            {
                rigid.velocity = Vector3.zero;
                transform.position = Vector3.Lerp(transform.position, player.position, 0.005f);
            }
            else
            {
                rigid.velocity = Vector3.zero;
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Weapon"))
    //    {
    //        if (isHit)
    //        {
    //            return;
    //        }
    //        Weapon weapon = collision.transform.GetComponent<Weapon>();
    //        if (weapon.isAttacking)
    //        {
    //            GetHit();
    //        }
    //    }
    //}

    public void GetHit()
    {
        if (isHit)
        {
            return;
        }

        isHit = true;
        stopVec = transform.position;

        hitCor = CorHit();
        StartCoroutine(hitCor);
    }

    private IEnumerator CorHit()
    {
        Debug.Log("공격맞음");
        anim.SetBool("isHit", true);

        yield return hitDelay;

        anim.SetBool("isHit", false);
        isHit = false;
        StopCoroutine(hitCor);
    }
}
