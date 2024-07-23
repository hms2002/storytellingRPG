using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : KeywordSup
{
    private void Awake()
    {
        keywordName = "가시 돋은";
        SetKeywordColor(B);
        keywordDamage = 3;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.
            pike, keywordDamage);
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
