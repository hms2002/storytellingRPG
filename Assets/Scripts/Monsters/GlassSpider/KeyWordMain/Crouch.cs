using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : KeywordMain
{
    private void Awake()
    {
        keywordName = "��ũ����";
        SetKeywordColor(BLUE);
        keywordProtect = 10;
        keyWordTension = 6;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = 10;
        caster.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
