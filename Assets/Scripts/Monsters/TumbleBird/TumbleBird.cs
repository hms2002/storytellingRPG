using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleBird : Actor
{
    TumbleBird()
    {
        _MAX_HP = 72;
    }

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

        additionalDamage += charactorState.GetStateStack(StateType.nextTurnDamage);
    }

}
