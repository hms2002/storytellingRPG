using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardy : KeywordSup
{
    TrasureDragon trasureDragon;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -4;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.dragonsTrasure -= 10;
        trasureDragon.trasureDamage += 10;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
