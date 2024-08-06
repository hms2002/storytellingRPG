using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleCrayfish_GripStrength : KeywordSup
{
    [Header("집게 악력 키워드 데미지")]
    [SerializeField] private int damage = 12;
    [Header("집게 악력 키워드 보호 수치에 따른 추가 데미지 최대 수치")]
    [SerializeField] private int plusDamage = 8;


    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "집게 악력";
        SetKeywordColor(R);
        keywordTension = -15;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.protect >= 8)
        {
            target.damage += damage + plusDamage;
        }
        else
        {
            target.damage += damage + caster.protect;
        }

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
