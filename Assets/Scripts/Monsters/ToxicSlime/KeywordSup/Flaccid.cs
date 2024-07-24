using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flaccid : KeywordSup
{
    ToxicSlime toxicSlime;

    [Header("부여되는 보호 스택의 양")]
    [SerializeField] private const int _amountOfProtectStack = 5;
    public int amountOfProtectStack { get { return _amountOfProtectStack; } }


    private void Awake()
    {
        keywordName = "흐물거리는";
        SetKeywordColor(B);
        keywordTension = -5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        toxicSlime = caster as ToxicSlime;

        toxicSlime.protect += amountOfProtectStack;

        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
