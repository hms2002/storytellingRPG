using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : KeywordMain
{
    private void Awake()
    {
        keywordName = "탕!";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        caster.dmgList.Add(keywordDamage);
        caster.charactorState.ReductionByValue(StateType.ammunition, buffStack);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
