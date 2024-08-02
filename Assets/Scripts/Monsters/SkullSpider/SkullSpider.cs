using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpider : Monster
{
    private void Awake()
    {
        MAX_HP = 60;
        hp = MAX_HP;
        encounterText = "스산한 기운이 느껴지자, 두개골 거미가 달그락 거리며 나타났다.";
    }
}
