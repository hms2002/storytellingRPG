using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_ScatterBarnacles : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "따개비 뿌리기";
        SetKeywordColor(Y);
        keywordTension = -30;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, debuffStack);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
