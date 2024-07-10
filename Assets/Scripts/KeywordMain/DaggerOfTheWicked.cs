using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerOfTheWicked : KeywordMain
{
    [SerializeField]
    private int extraDamage = 3;

    private void Awake()
    {
        keywordName = "악인의 단검";
        keywordDamage = 2;
        keywordProtect = 0;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        if (target.weakenStack > 0) // 타겟이 취약 상태라면
        {
            sentence.DamageControl(keywordDamage + extraDamage); // 키워드 데미지에 추가데미지 적용
        }
        else // 타겟이 취약 상태가 아니라면
        {
            sentence.DamageControl(keywordDamage); // 키워드 데미지만 적용
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
