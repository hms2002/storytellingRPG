using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -4;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(-2, 1);
        potionGlub.ColorChecking();
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
