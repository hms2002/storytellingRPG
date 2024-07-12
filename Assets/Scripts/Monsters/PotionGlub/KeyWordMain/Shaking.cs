using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -5;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.potionNum = Random.Range(-3, 3);
        potionGlub.ColorChecking();
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
