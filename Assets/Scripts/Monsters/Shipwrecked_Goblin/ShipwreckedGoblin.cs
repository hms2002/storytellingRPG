using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckedGoblin : Monster
{
    Animator anim;
    private void Awake()
    {
        MAX_HP = 70;
        hp = MAX_HP;
        encounterText = "조난당한 고블린이 나타났다.\n굉장히 고독해보인다.";
        anim = GetComponent<Animator>();
    }

    public void ThrowGibson()
    {
        anim.SetTrigger("ThrowGibson");
    }
}
