using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject floor = null;

    [SerializeField]
    private Transform map = null;

    void Start()
    {
        Make();
    }


    private void Make()
    {
        for(int i = 0; i < 64; i++)
        {
            for (int j = 0; j < 64; j++)
            {
                GameObject.Instantiate(floor, new Vector3(i, 0, j), Quaternion.identity, map);
            }
        }
    }
}
