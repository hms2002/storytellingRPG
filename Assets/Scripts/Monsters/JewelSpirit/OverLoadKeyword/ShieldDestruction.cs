using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShieldDestruction : KeywordMain
{
    [Header("없앤 보호 당 데미지 제어")]
    [SerializeField] private int DamagePerProtect = 5;
    private void Awake()
    {
        keywordName = "보호막 강타";
        SetKeywordColor(R);
        keywordDamage = 20;
        keywordProtect = 15;
        keywordTension = 35;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if(target.protect > 0)
        {
            caster.protect -= keywordProtect;
            caster.damage += DamagePerProtect * target.protect;
            target.protect = 0;
        }
        else
        {
            caster.damage = keywordDamage;
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
