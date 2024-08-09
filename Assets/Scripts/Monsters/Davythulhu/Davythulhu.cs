using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Davythulhu : Monster
{

    private void Awake()
    {
        MAX_HP = 94;
        hp = MAX_HP;
        encounterText = "불경하고 불운한 것들, 너희는 바다에 가라앉아도 불타오를 것이다.";
    }
}
