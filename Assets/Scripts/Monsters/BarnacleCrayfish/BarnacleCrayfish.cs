using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleCrayfish : Monster
{
    Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        MAX_HP = 80;
        protect = 10;
        hp = MAX_HP;
        encounterText = "강철같은 따개비를 두르고 있는 가재가 나타났다.";
        anim = GetComponent<Animator>();
    }

    //////////////////
    public override void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);
        keywordSup.Execute(this, target);
        keywordMain.Execute(this, target);
        Execute(target);
        SetAnimationState();
    }

    public override void Damaged(Actor attacker, int _damage)
    {
        base.Damaged(attacker, _damage);
        SetAnimationState();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        SetAnimationState();
    }

    private void SetAnimationState()
    {
        if (protect <= 8)
            anim.SetTrigger("Type_0");
        else if (1 <= protect && protect < 8)
            anim.SetTrigger("Type_1");
        else
            anim.SetTrigger("Type_2");
    }
}
