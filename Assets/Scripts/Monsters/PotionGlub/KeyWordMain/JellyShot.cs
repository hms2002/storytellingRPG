using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShot : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = 20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.isJellyShot = true;
        caster.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
