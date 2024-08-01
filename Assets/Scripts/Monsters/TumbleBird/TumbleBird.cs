using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleBird : Monster
{
    TumbleBird()
    {
        _MAX_HP = 72;
    }

    Animator anim;
    private bool _isContinuity = false;

    public bool isContinuity
    {
        get { return _isContinuity; }
        set { _isContinuity = value; }
    }
    
    public enum TumbleBirdBuffList
    {
        protect = StateType.protect,
        oneTimeReinforce = StateType.oneTimeReinforce,
        glassPragment = StateType.glassPragment,
        reduction = StateType.reduction,
        weaken = StateType.weaken,
        oneTimeProtect = StateType.oneTimeProtect,
    }

    private void Awake()
    {
        hp = MAX_HP; 
        encounterText = "날지 못하는 새는 구르는 방법을 배웠다. 그리고 더욱 위험해졌다. ";
        anim = GetComponent<Animator>();
    }

    public override void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        keywordSup.Execute(this, target);
        if(isContinuity)
        {
            keywordMain.Execute(this, target);
        }
        keywordMain.Execute(this, target);
        Execute(target);
        isContinuity = false;
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
        int buffCnt = charactorState.BuffCount();
        if (buffCnt < 1)
            anim.SetTrigger("Idle");
        else if (1 <= buffCnt && buffCnt < 3)
            anim.SetTrigger("Rolling1");
        else
            anim.SetTrigger("Rolling2");
    }

}
