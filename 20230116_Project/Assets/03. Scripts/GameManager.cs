using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TMP_Text countText = null;

    public int monsterCatchCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        monsterCatchCount = 0;
    }

    public void CatchCountUp()
    {
        monsterCatchCount++;
        countText.text = $"{monsterCatchCount}";
    }
}
