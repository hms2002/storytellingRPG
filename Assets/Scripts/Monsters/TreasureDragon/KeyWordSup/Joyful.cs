using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joyful : KeywordSup
{
    private void Awake()
    {
        keywordName = "즐거운";
        SetKeywordColor(RED);
        keywordDamage = 5;
        keywordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.nextTurnDamage, keywordDamage);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
