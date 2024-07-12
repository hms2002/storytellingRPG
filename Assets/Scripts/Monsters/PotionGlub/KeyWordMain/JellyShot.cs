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
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.isJellyShot = true;
        sentence.damage += keywordDamage;
        sentence.tension += keyWordTension;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
