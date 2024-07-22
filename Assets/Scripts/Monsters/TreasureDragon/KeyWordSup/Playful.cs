using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playful : KeywordSup
{
    TrasureDragon trasureDragon;


    private void Awake()
    {
        keywordName = "장난스러운";
        SetKeywordColor(BLUE);
        keywordTension = -4;
    }

    public override void Execute(Actor caster, Actor target)
    {
        trasureDragon = (TrasureDragon)caster;
        trasureDragon.charactorState.ReductionByValue(StateType.treasureOfDragon, 10);
        trasureDragon.trasureDamage += 10;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
