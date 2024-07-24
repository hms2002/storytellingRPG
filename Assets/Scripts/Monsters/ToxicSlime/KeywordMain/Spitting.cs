using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitting : KeywordMain
{
    ToxicSlime toxicSlime;

    [Header("부여되는 중독 스택의 양")]
    [SerializeField] private const int _amountOfAddictionStack = 2;
    public int amountOfAddictionStack { get { return _amountOfAddictionStack; } }


    private void Awake()
    {
        keywordName = "침 뱉기";
        SetKeywordColor(R);
        keywordDamage = 4;
        keywordTension = 10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        toxicSlime = caster as ToxicSlime;

        toxicSlime.damage += keywordDamage;

        target.charactorState.AddState(StateDatabase.stateDatabase.addiction, _amountOfAddictionStack);

        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
