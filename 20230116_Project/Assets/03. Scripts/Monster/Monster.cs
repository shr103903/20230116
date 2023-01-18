using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Transform playerTransform = null;

    public CreateMonster monsterManager = null;

    public Vector3 attackScale = Vector3.zero;

    [SerializeField]
    private Animator anim = null;

    [SerializeField]
    private GameObject hpPanel = null;

    [SerializeField]
    private Transform attackColl = null;

    [SerializeField]
    private Image hpBar = null;

    [SerializeField]
    private float attackPower = 1.0f;

    [SerializeField]
    private float distance = 0;

    private PlayerMove player = null;

    private Rigidbody rigid = null;

    private Collider coll = null;

    private Vector3 stopVec = Vector3.zero;

    private float speed = 1.0f;

    private bool isMove = false;

    private bool isAttacking = false;

    private bool isHit = false;

    private bool isDead = false;

    private bool hpBarActive = false;

    private IEnumerator attackCor = null;

    private WaitForSeconds attackDelay = new WaitForSeconds(3.0f); 

    private IEnumerator hitCor = null;

    private WaitForSeconds hitDelay = new WaitForSeconds(1.0f); 

    
    private IEnumerator dieCor = null;

    private WaitForSeconds dieDelay = new WaitForSeconds(10.0f);

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        player = playerTransform.GetComponent<PlayerMove>();
        hpBarActive = false;

        attackScale = attackColl.localScale;

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

    public void Init()
    {
        hpBarActive = false;
        hpBar.fillAmount = 1.0f;
        isHit = false;
        isDead = false;
        if(hitCor != null)
        {
            StopCoroutine(hitCor);
        }
    }

    private void Move()
    {
        if (playerTransform != null)
        {
            if (isHit || isDead)
            {
                return;
            }

            if (transform.position.y <= -1 || transform.position.y >= 1)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 0.05f);
            distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance < 1.5f)
            {
                if (isMove)
                {
                    isMove = false;
                    anim.SetBool("IsMove", false);
                }
                else if (!isAttacking)
                {
                    isAttacking = true;
                    attackCor = CorAttack();
                    StartCoroutine(attackCor);
                }
            }
            else if (distance >= 1.5f && distance < 10.0f)
            {
                if (!isMove)
                {
                    isMove = true;
                    anim.SetBool("IsMove", true);
                }

                rigid.velocity = Vector3.zero;
                transform.position += transform.forward * speed * 0.6f * Time.deltaTime;
            }
            else
            {
                if (!isMove)
                {
                    isMove = true;
                    anim.SetBool("IsMove", true);
                }

                rigid.velocity = Vector3.zero;
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }

    public void GetHit(float power)
    {
        if (!hpBarActive)
        {
            hpBarActive = true;
            hpPanel.SetActive(true);
        }
        hpBar.fillAmount -= 0.1f * power;
        if(hpBar.fillAmount <= 0)
        {
            Dead();
            return;
        }

        if (isHit)
        {
            return;
        }
        else if (isAttacking)
        {
            anim.SetBool("IsAttack", false);
            isAttacking = false;
        }

        isHit = true;
        stopVec = transform.position;

        hitCor = CorHit(power);
        StartCoroutine(hitCor);
    }

    public void Attack()
    {
        if (Physics.OverlapBox(attackColl.position, attackScale, transform.rotation, 1 << 7).Length > 0)
        {
            player.GetHit(attackPower);
        }
    }

    private void Dead()
    {
        isDead = true;
        anim.SetBool("IsDead", true);
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        coll.isTrigger = true;
        hpBarActive = false;
        hpPanel.SetActive(false);

        dieCor = CorDead();
        StartCoroutine(dieCor);
    }

    private IEnumerator CorAttack()
    {
        yield return attackDelay;

        if (isHit)
        {
            anim.SetBool("IsAttack", false);
        }
        else
        {
            anim.SetBool("IsAttack", true);
        }

        yield return attackDelay;

        isAttacking = false;
        StopCoroutine(attackCor);
    }

    private IEnumerator CorHit(float power)
    {
        Debug.Log("공격맞음");
        anim.SetBool("IsHit", true);

        yield return hitDelay;

        anim.SetBool("IsHit", false);
        isHit = false;
        StopCoroutine(hitCor);
    }

    private IEnumerator CorDead()
    {
        monsterManager.MonsterKilled();

        yield return dieDelay;
        monsterManager.DeleteMonster(this.gameObject);
        StopCoroutine(dieCor);
    }
}
