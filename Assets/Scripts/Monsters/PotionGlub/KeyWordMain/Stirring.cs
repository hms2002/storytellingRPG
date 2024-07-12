using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stirring : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -8;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(1, 3);
        potionGlub.ColorChecking();
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}

