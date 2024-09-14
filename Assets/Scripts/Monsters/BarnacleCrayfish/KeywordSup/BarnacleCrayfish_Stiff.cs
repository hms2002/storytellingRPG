using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_Stiff : KeywordSup
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "딱딱한";
        SetKeywordColor(B);
        keywordTension = 8;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReinforce, buffStack);
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
