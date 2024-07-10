using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAtTheStake : KeywordMain
{
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        target.Damaged( target.burnStack , DamageType.Beat);
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
