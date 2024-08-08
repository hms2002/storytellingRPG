using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_Wave : KeywordMain
{
    private void Awake()
    {
        keywordName = "파도";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        if(caster is RogueWave)
        {
            RogueWave child = (RogueWave)caster;
            child.AddKeyword();
        }
        caster.tension += keywordTension;
    }
}
