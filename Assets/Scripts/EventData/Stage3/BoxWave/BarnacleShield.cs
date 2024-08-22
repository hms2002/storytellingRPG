using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleShield : KeywordMain
{
    [Header("자해데미지")]
    [SerializeField]
    int selfDamage;
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
        caster.hp -= selfDamage;
        caster.protect += keywordProtect;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
