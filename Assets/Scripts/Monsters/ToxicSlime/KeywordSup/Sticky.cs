using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : KeywordSup
{
    [Header("부여되는 중독 스택양")]
    [SerializeField] private int _addictionStack = 1;        // 중독 스택


    private void Awake()
    {
        keywordName = "끈적이는";
        SetKeywordColor(RED);
        keywordTension = 5;
    }

    public override void Execute(Actor caster, Actor target)
    {

    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
