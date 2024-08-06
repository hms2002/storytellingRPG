using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_PincerStrike : KeywordMain
{
    [Header("집게 강타 키워드 데미지 수치")]
    [SerializeField] private int damage = 8;

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
            target.damage += damage + caster.protect;
            caster.protect = 0;
        }
        else
        {
            target.damage += damage + caster.protect;
        }

        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
