using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster
{
    private void Awake()
    {
        MAX_HP = 42;
        hp = MAX_HP;
        encounterText = "왜 나타나지 않는지 의문이 들 때, 그것들은 항상 나타난다.";
    }
}
