using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeShield : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target) 
    {
        caster.protect += keywordProtect;
        caster.hp += keywordHeal;
    }

    public override void Check(KeywordSup keywordSup)
    {

    }
}
