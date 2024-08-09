using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle : Monster
{

    private void Awake()
    {
        MAX_HP = 100;
        hp = MAX_HP;
        encounterText = "악마 같은 모습의 석상이 길을 가로막고 있다.";
    }
    private void Start()
    {
        protect += 30;
    }
}
