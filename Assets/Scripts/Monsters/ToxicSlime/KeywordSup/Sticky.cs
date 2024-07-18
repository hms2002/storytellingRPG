using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : KeywordSup
{
    [Header("부여되는 중독 스택의 양")]
    [SerializeField] private const int _amountOfAddictionStack = 1;
    public int amountOfAddictionStack { get { return _amountOfAddictionStack; } }


    private void Awake()
    {
        keywordName = "끈적이는";
        SetKeywordColor(RED);
        keywordTension = 5;
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (target.addictionStack <= 8)
        {
            target.addictionStack += amountOfAddictionStack;
        }

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
