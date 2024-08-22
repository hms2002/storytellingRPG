using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : KeywordMain
{
    [Header("연타 데미지 조절")]
    [SerializeField]
    int firstAttack;
    [SerializeField]
    int secondAttack;
    [SerializeField]
    int thirdAttack;
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        for (int i = 0; i <= caster.repeatStack; i++)
        {
            target.Damaged(caster, new DamageInfo(firstAttack));
            target.Damaged(caster, new DamageInfo(secondAttack));
            target.Damaged(caster, new DamageInfo(thirdAttack));
        }

        EffectManager.instance.PlayEffect(EffectManager.EffectType.Combo, target, 3 * caster.repeatStack);
    }

    public override void Check(KeywordSup keywordSup) { }
}
