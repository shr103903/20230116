using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    public PlayerAttack playerAttack = null;

    public PlayerMove playerMove = null;

    [SerializeField]
    private GameObject atkItem = null;

    [SerializeField]
    private GameObject weaponItem = null;

    private GameObject item = null;

    private float createDelay = 5.0f;

    private float randX = 0;

    private float randZ = 0;

    private IEnumerator createCor = null;

    private System.Random random = new System.Random();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        createCor = CorCreate();
        StartCoroutine(createCor);
    }

    public void StopCreation()
    {
        StopCoroutine(createCor);
    }

    private IEnumerator CorCreate()
    {
        while (true)
        {
            createDelay = random.Next(4, 8);
            yield return new WaitForSeconds(createDelay);

            CreateItemBox();
        }
    }

    private void CreateItemBox()
    {
        randX = random.Next(5, 60);
        randZ = random.Next(5, 60);

        int randNum = random.Next(2);

        item = GameObject.Instantiate(randNum == 0 ? atkItem : weaponItem, null);
        item.transform.position = new Vector3(randX, 0.576092f, randZ);
    }
}
