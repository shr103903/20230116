using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMonster : MonoBehaviour
{
    public float turn = 0.3f;

    [SerializeField]
    private Text countText = null;

    [SerializeField]
    private Transform player = null;

    [SerializeField]
    private int monsterCount = 0;

    [SerializeField]
    private float yPos = 0;

    private ObjectPool monsterPool = null;

    private Vector3 spawnPos = Vector3.zero;

    private float randX = 0;

    private float randZ = 0;

    private bool isStart = true;

    private IEnumerator createCor = null;

    private WaitForSeconds spawnDelay = new WaitForSeconds(1.0f);

    private System.Random random = new System.Random();

    private void Awake()
    {
        monsterPool = GetComponent<ObjectPool>();
        monsterCount = 0;
        isStart = true;

        for (int i = 0; i < 10; i++)
        {
            InitMonster();
        }

        createCor = CorCreateMonster();
        StartCoroutine(createCor);
    }

    public void InitMonster()
    {
        do
        {
            randX = random.Next(0, 64);
            randZ = random.Next(0, 64);
            spawnPos.Set(randX, yPos, randZ);
        } while (Physics.OverlapSphere(spawnPos, 1.0f, 1 << 6).Length > 0);

        GameObject monster = monsterPool.GetObject();
        monster.transform.position = spawnPos;
        Monster monsterData = monster.GetComponent<Monster>();
        if (monsterData.playerTransform == null)
        {
            monsterData.monsterManager = this;
            monsterData.playerTransform = this.player;
        }
        monsterData.Init();

        monsterCount++;
    }

    public void DeleteMonster(GameObject monster)
    {
        monsterCount--;
        monsterPool.ReturnObject(monster);
        InitMonster();
    }

    public void MonsterKilled()
    {
        int count = int.Parse(countText.text);
        countText.text = $"{++count}";
    }

    private IEnumerator CorCreateMonster()
    {
        if (isStart)
        {
            isStart = false;
            yield return new WaitForSeconds(turn);
        }

        while(monsterCount < 70)
        {
            yield return spawnDelay;

            InitMonster();
        }

        StopCoroutine(createCor);
    }
}
