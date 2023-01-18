using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMethod : MonoBehaviour
{
    public bool isMonster = false;

    [SerializeField]
    private Monster monster = null;

    private Animator anim = null;

    private void Awake()
    {
        if (isMonster)
        {
            anim = GetComponent<Animator>();
        }
    }

    public void MonsterHit()
    {
        anim.SetBool("IsHit", false);
    }

    public void MonsterAttack()
    {
        monster.Attack();
    }

    public void FinishAttack()
    {
        //monster.isAttacking = false;
        anim.SetBool("IsAttack", false);
    }
}
