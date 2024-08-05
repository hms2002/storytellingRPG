using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addicted : KeywordSup
{
    [Header("중독 스택 데미지 배수")]
    [SerializeField] private const float _addictionDamageMultiplexes = 2.0f;
    public float addictionDamageMultiplexes { get { return _addictionDamageMultiplexes; } }

    private void Awake()
    {
        keywordName = "중독된";
        SetKeywordColor(Y);
        keywordTension = 15;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
