using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterKnight_Ignited : KeywordSup
{
    private void Awake()
    {
        keywordName = "점화된";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Plus(keywordDamage);
        caster.charactorState.AddState(StateType.burn, debuffStack);
        target.charactorState.AddState(StateType.burn, debuffStack);
        caster.tension += keywordTension;
    }
}
