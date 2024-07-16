using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : KeywordMain
{
    private void Awake()
    {
        keywordName = "¿õÅ©¸®±â";
        SetKeywordColor(BLUE);
        keywordProtect = 10;
        keywordTension = 6;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = 10;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
