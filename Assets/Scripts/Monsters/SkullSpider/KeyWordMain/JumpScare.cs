using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : KeywordMain
{
    private void Awake()
    {
        keywordName = "점프스케어";

        SetKeywordColor(RED);
        keywordTension = 3;
        keywordDamage = 7;
    }
    
    public override void Execute(Actor caster, Actor target)
    {
        target.damage += keywordDamage * target.fearStack;
        caster.tension += keywordTension * target.fearStack;
        target.fearStack = 0;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
