using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShot : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(B);
        keywordTension = 20;
    }

    public override void Execute(Actor caster, Actor target)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.isJellyShot = true;
        caster.tension += keywordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
