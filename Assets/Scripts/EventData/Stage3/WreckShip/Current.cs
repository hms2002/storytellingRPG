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
        caster.dmgList.Add(firstAttack);
        caster.dmgList.Add(secondAttack);
        caster.dmgList.Add(thirdAttack);
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
