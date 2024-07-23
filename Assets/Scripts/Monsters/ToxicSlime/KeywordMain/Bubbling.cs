using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbling : KeywordMain
{
    ToxicSlime toxicSlime;

    [Header("부여되는 중독 스택의 양")]
    [SerializeField] private const int _amountOfAddictionStack = 1;
    public int amountOfAddictionStack { get { return _amountOfAddictionStack; } }


    private void Awake()
    {
        keywordName = "보글거리기";
        SetKeywordColor(B);
        keywordDamage = 2;
        keywordTension = 5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        toxicSlime = caster as ToxicSlime;

        toxicSlime.damage = keywordDamage;
        
        target.charactorState.AddState
            (StateDatabase.stateDatabase.addiction, amountOfAddictionStack);
        
        target.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}