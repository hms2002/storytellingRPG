using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMaster_RootArmor : KeywordMain
{
    private void Awake()
    {
        keywordName = "뿌리 갑옷";
        SetKeywordColor(B);
        keywordProtect = 4;
        keywordTension = -10;

        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(caster.charactorState.GetStateStack(StateType.multiplication) >= 2)
        {
            caster.charactorState.ReductionByValue(StateType.multiplication, 2);
            keywordProtect = 7;
        }
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
