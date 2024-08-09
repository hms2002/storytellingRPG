using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealedGargoyle_TailBang : KeywordMain
{
    private void Awake()
    {
        keywordName = "꼬리 강타";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.Damaged(caster, keywordDamage);
        target.charactorState.AddState(StateType.oneTimeReduction, debuffStack);
        caster.tension += keywordTension;
    }
}
