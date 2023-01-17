using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMethod : MonoBehaviour
{
    public bool isMonster = false;

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
        anim.SetBool("isHit", false);
    }
}
