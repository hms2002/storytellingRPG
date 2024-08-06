using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHat : KeywordMain
{
    [Header("안전모 키워드 쉴드 수치")]
    [SerializeField] private int amountOfProtect = 10;

    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "안전모";
        SetKeywordColor(B);
        keywordTension = -15;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += amountOfProtect;
        caster.tension += keywordProtect;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
