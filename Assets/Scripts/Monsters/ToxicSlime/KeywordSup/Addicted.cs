using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addicted : KeywordSup
{
    ToxicSlime toxicSlime;

    [Header("중독 스택 데미지 배수")]
    [SerializeField] private const float _addictionDamageMultiplexes = 2.0f;
    public float addictionDamageMultiplexes { get { return _addictionDamageMultiplexes; } }


    private void Awake()
    {
        keywordName = "중독된";
        SetKeywordColor(RED);
        keywordTension = 15;
    }

    public override void Execute(Actor caster, Actor target)
    {

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
