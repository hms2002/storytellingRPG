using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_PincerStrike : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "집게 강타";
        SetKeywordColor(R);
        keywordTension = 20;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(caster.protect >= 9)
        {
            caster.dmgList.Add(keywordDamage + caster.protect);
            caster.protect = 0;
        }
        else
        {
            caster.damage += keywordDamage + caster.protect;
        }

        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
