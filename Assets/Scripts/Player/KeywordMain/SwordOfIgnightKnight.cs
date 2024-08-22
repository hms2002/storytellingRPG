using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfIgnightKnight : KeywordMain
{
    
    private void Awake()
    {
        keywordName = "점화 기사의 검";
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        if(target.charactorState.GetStateStack(StateType.burn) >= 8)
            caster.dmgList.Add(keywordDamage);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
