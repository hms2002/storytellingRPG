using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerOfTheWicked : KeywordMain
{
    [Header("악인의 단검 추가 데미지")]
    [SerializeField] private int extraDamage = 3;


    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
        caster.dmgList.Add(target.charactorState.GetStateStack(StateType.weaken) * 3);
    }

    public override void Check(KeywordSup _keywordSup) { }
}
