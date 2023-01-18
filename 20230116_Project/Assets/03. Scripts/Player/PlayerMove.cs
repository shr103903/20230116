using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    public Ease dodgeEase = Ease.OutCubic;

    public bool isDodge = false;

    public float speed = 5f;

    [SerializeField]
    private PlayerAttack playerAttack = null;

    [SerializeField]
    private Animator anim = null;

    [SerializeField]
    private Image hpBar = null;

    private Transform playerTransform = null;

    private Vector3 moveVec = Vector3.zero;

    private float moveScale = 0;

    private float hp = 1.0f;

    //private float horizontal = 0;

    //private float vertical = 0;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        //컴퓨터 확인용 이동키
        //Move(0,0);
    }

    public void Attack()
    {
        if (playerAttack.isAttacking || isDodge)
        {
            return;
        }

        playerAttack.Attack();
    }

    public void Dodge()
    {
        if (isDodge || playerAttack.isAttacking)
        {
            return;
        }

        isDodge = true;
        anim.SetBool("IsDodge", true);
        playerTransform.DOMove(playerTransform.position + playerTransform.forward * 7f, 1.2f).SetEase(dodgeEase).OnComplete(() => 
        {
            FinishDodge();
        });
    }

    private void FinishDodge()
    {
        anim.SetBool("IsDodge", false);
        isDodge = false;
    }

    public void Move(float horizontal, float vertical)
    {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        if (isDodge)
        {
            return;
        }

        moveVec.Set(horizontal, 0, vertical);

        moveScale = moveVec.magnitude;
        if (moveScale != 0)
        {
            playerTransform.position = playerTransform.position + moveVec * speed * Time.deltaTime;
            Turn(moveVec);
            anim.SetFloat("moveFloat", moveScale);
        }
    }

    public void StopMove()
    {
        anim.SetFloat("moveFloat", 0);
    }

    public void GetHit(float power)
    {
        if (isDodge)
        {
            return;
        }

        hp -= 0.01f * power;
        hpBar.fillAmount = hp;
    }

    public void Hill()
    {
        hp += 0.2f;
        hpBar.fillAmount = hp;
    }

    private void Turn(Vector3 moveVec)
    {
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.LookRotation(moveVec), 0.1f);
    }
}
