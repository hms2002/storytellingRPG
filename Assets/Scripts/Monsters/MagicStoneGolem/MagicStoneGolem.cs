using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStoneGolem : Monster
{
    private void Awake()
    {
        MAX_HP = 100;
        hp = MAX_HP;
        encounterText = "대지가 진동하며 바위가 움직였다.";
    }
}
