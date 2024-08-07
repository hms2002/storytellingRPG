using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark : Monster
{
    // Start is called before the first frame update
    private void Awake()
    {
        MAX_HP = 150;
        hp = MAX_HP;
        encounterText = "바다위의 포식자 상어가 나타났다.\n감기가 걸린 것 같다..?";
    }
}
