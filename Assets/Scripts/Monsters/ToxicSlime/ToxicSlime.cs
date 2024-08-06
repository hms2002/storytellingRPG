using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSlime : Monster
{
    private void Awake()
    {
        MAX_HP = 70;
        hp = MAX_HP;
        encounterText = "유독성 슬라임이 보글거리며 나타났다.";
    }
};
