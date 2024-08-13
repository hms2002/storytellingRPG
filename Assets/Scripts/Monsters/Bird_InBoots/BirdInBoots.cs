using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots : Monster
{
    private void Awake()
    {
        MAX_HP = 94;
        hp = MAX_HP;
        encounterText = "날지 못하는 모든 새를 대신해 말하겠소. 당장 장화를 내놓으시오!";
    }
}
