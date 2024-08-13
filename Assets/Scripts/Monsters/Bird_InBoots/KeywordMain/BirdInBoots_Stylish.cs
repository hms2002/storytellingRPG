using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Stylish : KeywordMain
{
    private void Awake()
    {
        keywordName = "멋부림";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
    }
}
