using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantastic : KeywordSup
{
    TrasureDragon trasureDragon;
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        trasureDragon = caster as TrasureDragon;
        trasureDragon.dragonTrasure -= 20;
        trasureDragon.trasureDamage += 20;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
