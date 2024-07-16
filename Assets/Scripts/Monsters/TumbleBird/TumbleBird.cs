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

    public int glassFragmentStack
    {
        get { return _glassFragmentStack; }
        set
        {
            _glassFragmentStack = value;
            stateUIController.GlassFragmentOn(_glassFragmentStack);
        }
    }

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

    private void Awake()
    {
        hp = MAX_HP;
        tumbleBirdsBuffList = new int[] {protect, oneTimeReinforce, glassFragmentStack, reductionStack, weakenStack, oneTimeProtect};
        allStateList = new int[] {protect, oneTimeProtect, additionalStack, additionalDamage, oneTimeReinforce, pike,
            burnStack, venomStack, reductionStack, weakenStack,glassFragmentStack};
    }

    public override void Action(Actor target)
    {
        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        additionalDamage += nextTurnDamage;

        keywordSup.Execute(this, target);
        if(isContinuity)
        {
            keywordMain.Execute(this, target);
        }
        keywordMain.Execute(this, target);
        Execute(target);
        isContinuity = false;
    }

    public int BuffCount()
    {
        int buffCount = 0;
        for(int i = 0; i < allStateList.Length; i++)
        {
            if(allStateList[i] > 0)
            {
                buffCount++;
            }
        }
        return buffCount;
    }
}
