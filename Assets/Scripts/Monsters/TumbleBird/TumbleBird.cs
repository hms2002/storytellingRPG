using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleBird : Actor
{
    TumbleBird()
    {
        _MAX_HP = 72;
    }

    private int _glassFragmentStack = 0;
    private bool _isContinuity = false;
    private int[] _tumbleBirdsBuffList;


    public int[] tumbleBirdsBuffList
    {
        get { return _tumbleBirdsBuffList; }
        set { _tumbleBirdsBuffList = value; }
    }

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
        size
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
