using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask monsterLayer = new LayerMask();

    public bool isAttacking = false;

    public float attackPower = 1.0f;

    [SerializeField]
    private PlayerMove player = null;

    [SerializeField]
    private Transform smallAttackPos = null;

    [SerializeField]
    private Transform bigAttackPos = null;

    private Animator anim = null;

    private FollowCam cam = null;

    private int attackCount = 0;

    private IEnumerator comboCor = null;

    private WaitForSeconds comboDelay = new WaitForSeconds(1.2f);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main.GetComponent<FollowCam>();

        attackCount = 1;
    }

    public void FinishAttack()
    {
        Debug.Log("공격 끝");
        if (attackCount == 5)
        {
            attackCount = 0;
        }
        attackCount++;

        anim.SetBool("IsAttack", false);
        isAttacking = false;
        player.speed *= 5f;
    }

    public void Attack()
    {
        isAttacking = true;
        player.speed *= 0.2f;
        anim.SetBool("IsAttack", true);
        anim.SetFloat("attackCombo", attackCount * 0.1f);


        if (comboCor != null)
        {
            StopCoroutine(comboCor);
        }
        comboCor = CorCombo();
        StartCoroutine(comboCor);
    }

    public void AttackHit(int count)
    {
        if (attackCount >= 4)
        {
            AttackMonster(bigAttackPos, 2.0f);
        }
        else 
        {
            AttackMonster(smallAttackPos, 1.5f);
        }
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
        Collider[] Coll = Physics.OverlapSphere(pos.position, radius, monsterLayer);

        if (Coll.Length == 0)
        {
            return;
        }

        foreach (Collider obj in Coll)
        {
            if (obj.CompareTag("Monster"))
            {
                obj.GetComponent<Monster>().GetHit(attackPower * attackCount * 0.5f);
            }
            else
            {
                obj.GetComponent<Item>().OpenItemBox();
            }
        }

        cam.Shake();
    }
}
