using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeakAttack : KeywordMain
{
    private void Awake()
    {
        keywordName = "�θ� ����";
        SetKeywordColor(BLUE);
        keywordProtect = 30;
        keywordTension = -24;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
