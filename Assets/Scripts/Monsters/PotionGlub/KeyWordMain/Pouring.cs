using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : KeywordMain
{
    PotionGlub potionGlub;
    private void Awake()
    {
        SetKeywordColor(RED);
        keyWordTension = 10;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        potionGlub = caster as PotionGlub;
        potionGlub.PotionHitted(target);
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}