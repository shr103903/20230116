using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerMove player = null;

    [SerializeField]
    private Weapon weapon = null;

    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FinishAttack()
    {
        Debug.Log("공격 끝");
        anim.SetBool("isAttack", false);
        player.isAttacking = false;
        weapon.isAttacking = false;
    }
}
