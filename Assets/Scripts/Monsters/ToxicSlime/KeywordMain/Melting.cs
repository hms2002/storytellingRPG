using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : KeywordMain
{
    [Header("녹이기 키워드 수치 제어")]
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
        caster.damage += target.addictionStack * damagePerAddictionStack;

        target.tension += target.addictionStack * keywordTension;

        target.addictionStack = 0;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
