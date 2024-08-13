using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight : Monster
{
    private void Awake()
    {
        MAX_HP = 64;
        hp = MAX_HP;
        encounterText = "불 속에서 타지 않고 견딜 수 있는 인간은 없다.";
    }
}
