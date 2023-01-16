using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateMonster : MonoBehaviour
{
    [SerializeField]
    private int monsterCount = 0;

    private ObjectPool monsterPool = null;

    private Vector3 spawnPos = Vector3.zero;

    private float randX = 0;

    private float randZ = 0;

    private IEnumerator createCor = null;

    private WaitForSeconds spawnDelay = new WaitForSeconds(0.2f);

    private System.Random random = new System.Random();

    private void Awake()
    {
        monsterPool = GetComponent<ObjectPool>();
        monsterCount = 0;
    }

    private void Start()
    {
        for(int i = 0; i < 50; i++)
        {
            InitMonster();
        }

        createCor = CorCreateMonster();
        StartCoroutine(createCor);
    }

    private void InitMonster()
    {
        randX = random.Next(0, 64);
        randZ = random.Next(0, 64);
        spawnPos.Set(randX, 0, randZ);

        GameObject monster = monsterPool.GetObject();
        monster.transform.position = spawnPos;

        monsterCount++;
    }

    private IEnumerator CorCreateMonster()
    {
        while(monsterCount < 150)
        {
            yield return spawnDelay;

            InitMonster();
        }

        StopCoroutine(createCor);
    }
}
