using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playful : KeywordSup
{
    TrasureDragon trasureDragon;
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.dragonTrasure -= 10;
        trasureDragon.trasureDamage += 10;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
