using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;
using static Keyword;

public class BarnacleCrayfish_Covered : KeywordSup
{
    [Header("뒤덮인 키워드 보호 수치")]
    [SerializeField] private int protecte = 17;

    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "뒤덮인";
        SetKeywordColor(B);
        keywordTension = 20;
        keywordProtect = protecte;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
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
