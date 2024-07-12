using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : KeywordSup
{
    TrasureDragon trasureDragon;
    private void Awake()
    {
        SetKeywordColor(BLUE);
        keyWordTension = -11;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.dragonTrasure -= 20;
        trasureDragon.trasureDamage += 20;
        sentence.tension += keyWordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
