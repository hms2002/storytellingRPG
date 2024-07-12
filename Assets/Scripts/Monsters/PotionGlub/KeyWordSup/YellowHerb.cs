using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHerb : KeywordSup
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -10;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum += Random.Range(2, 4);
        potionGlub.ColorChecking();
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}