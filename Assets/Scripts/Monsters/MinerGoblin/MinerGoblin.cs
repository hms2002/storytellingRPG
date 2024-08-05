using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerGoblin : Monster
{
    private void Awake()
    {
        MAX_HP = 50;
        hp = MAX_HP;
        encounterText = "길을 잃은 광부 고블린이 나타났다.\n자신을 놓고 간 동료들에게 큰 배신감을 느낀 것 같다.";
    }
}
