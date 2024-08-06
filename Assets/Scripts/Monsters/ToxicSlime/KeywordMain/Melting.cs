using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : KeywordMain
{
    private void Awake()
    {
        keywordName = "녹이기";
        SetKeywordColor(B);
        keywordTension = -10;
        keywordDamage = 4;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int addictionStack = target.charactorState.GetStateStack(StateType.addiction);
        caster.damage += addictionStack * keywordDamage;

        caster.tension += addictionStack * keywordTension;

        target.charactorState.ResetState(StateType.addiction);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
