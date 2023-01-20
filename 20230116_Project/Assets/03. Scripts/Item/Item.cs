using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private bool isWeaponBox = false;

    private PlayerAttack playerAttack = null;

    private PlayerMove playerMove = null;

    private void Awake()
    {
        //playerAttack = ItemManager.instance.playerAttack;
        //playerMove = ItemManager.instance.playerMove;
    }

    public void OpenItemBox()
    {
        if (isWeaponBox)
        {
            //무기 생성 또는 강화
        }
        else
        {
            //공격력 강화 + 재화 얻음
            playerAttack.attackPower++;
            playerMove.Hill();
        }

        Destroy(this.gameObject);
    }
}
