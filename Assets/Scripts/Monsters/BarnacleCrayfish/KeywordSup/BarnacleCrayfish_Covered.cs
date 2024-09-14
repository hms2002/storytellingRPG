using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_Covered : KeywordSup
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "뒤덮인";
        SetKeywordColor(B);
        keywordTension = 20;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
