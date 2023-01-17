using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isAttacking = false;

    public LayerMask monsterLayer = new LayerMask();

    [SerializeField]
    private Weapon weapon = null;

    [SerializeField]
    private Animator anim = null;

    [SerializeField]
    private Transform smallAttackPos = null;

    [SerializeField]
    private Transform bigAttackPos = null;

    [SerializeField]
    private float speed = 10f;

    private Transform playerTransform = null;

    private Vector3 moveVec = Vector3.zero;

    private int attackCount = 0;

    private float moveScale = 0;

    //private float horizontal = 0;

    //private float vertical = 0;

    private IEnumerator comboCor = null;

    private WaitForSeconds comboDelay = new WaitForSeconds(1.2f);

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        attackCount = 1;
    }

    private void Update()
    {
        //컴퓨터 확인용 이동키
        //Move(0,0);
    }

    public void Attack()
    {
        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
        weapon.isAttacking = true;
        anim.SetBool("isAttack", true);
        anim.SetFloat("attackCombo", attackCount * 0.1f);
        if(attackCount <= 3)
        {
            AttackMonster(smallAttackPos, 0.8f);
        }
        else
        {
            AttackMonster(bigAttackPos, 1.5f);
        }

        if (attackCount == 5)
        {
            attackCount = 0;
        }
        attackCount++;

        if(comboCor != null)
        {
            StopCoroutine(comboCor);
        }
        comboCor = CorCombo();
        StartCoroutine(comboCor);
    }

    public void Move(float horizontal, float vertical)
    {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
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

    private void Turn(Vector3 moveVec)
    {
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.LookRotation(moveVec), 0.1f);
    }

    private IEnumerator CorCombo()
    {
        yield return comboDelay;
        if (!isAttacking)
        {
            attackCount = 1;
        }
        StopCoroutine(comboCor);
    }

    private void AttackMonster(Transform pos, float radius)
    {
        Collider[] monsterColl = Physics.OverlapSphere(pos.position, radius, monsterLayer);
        foreach(Collider monster in monsterColl)
        {
            monster.GetComponent<Monster>().GetHit();
        }
    }
}
