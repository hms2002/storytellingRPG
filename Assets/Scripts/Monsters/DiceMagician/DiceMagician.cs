using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician : Monster
{
    private void Awake()
    {
        MAX_HP = 77;
        hp = MAX_HP;
        encounterText = "달그락거리는 그것의 머리는 불확실성의 집합이다.";
    }
}
