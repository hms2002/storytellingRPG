using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : KeywordMain
{
    ToxicSlime toxicSlime;

    [Header("중독 스택 당 데미지")]
    [SerializeField] private const int _damagePerAddictionStack = 4;
    public int damagePerAddictionStack { get { return _damagePerAddictionStack; } }


    private void Awake()
    {
        keywordName = "녹이기";
        SetKeywordColor(BLUE);
        keywordTension = -10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        toxicSlime = caster as ToxicSlime;

        toxicSlime.damage += target.addictionStack * damagePerAddictionStack;

        target.addictionStack = 0;

        target.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
